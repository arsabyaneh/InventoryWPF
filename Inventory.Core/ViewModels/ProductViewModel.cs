using Inventory.Core.Exceptions;
using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.EntityFramework.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly IProductService _ProductService;
        private readonly ProductStore _ProductStore;
        private readonly PriceStore _PriceStore;

        private string _Title;
        private string _Code;
        private string _BrandTitle;
        private string _NewPriceBuy;
        private string _NewPriceSell;
        private DateTime _NewPriceDate = DateTime.Now;
        private string _CurrentSellPrice;
        private string _CurrentBuyPrice;
        private ObservableCollection<PriceViewModel> _Prices = new ObservableCollection<PriceViewModel>();
        private Product _Product;

        public ProductViewModel(INavigationService navigationService, IProductService productService, 
            ProductStore productStore, PriceStore priceStore, Product product)
        {
            _NavigationService = navigationService;
            _ProductService = productService;
            _ProductStore = productStore;
            _PriceStore = priceStore;

            _PriceStore.PriceDeleted += PriceStore_PriceDeleted;

            Product = product;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            AddPriceCommand = new RelayCommand(AddPrice);
            ViewCommand = new RelayCommand(View);
            DeleteCommand = new RelayParameterizedCommand(Delete);
        }

        public string Title { get => _Title; set => SetProperty(ref _Title, value); }
        public string Code { get => _Code; set => SetProperty(ref _Code, value); }
        public string BrandTitle { get => _BrandTitle; set => SetProperty(ref _BrandTitle, value); }
        public string NewPriceBuy { get => _NewPriceBuy; set => SetProperty(ref _NewPriceBuy, value); }
        public string NewPriceSell { get => _NewPriceSell; set => SetProperty(ref _NewPriceSell, value); }
        public DateTime NewPriceDate { get => _NewPriceDate; set => SetProperty(ref _NewPriceDate, value); }
        public string CurrentSellPrice { get => _CurrentSellPrice; set => SetProperty(ref _CurrentSellPrice, value); }
        public string CurrentBuyPrice { get => _CurrentBuyPrice; set => SetProperty(ref _CurrentBuyPrice, value); }
        public ObservableCollection<PriceViewModel> Prices { get => _Prices; set => SetProperty(ref _Prices, value); }

        private ObservableCollection<PriceViewModel> RemovedPrices { get; set; } = new ObservableCollection<PriceViewModel>();

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddPriceCommand { get; }
        public ICommand ViewCommand { get; }
        public ICommand DeleteCommand { get; }

        public Product Product
        {
            get
            {
                _Product = new Product
                {
                    Id = _Product != null ? _Product.Id : 0,
                    Title = Title,
                    Code = Code,
                    Brand = new Brand
                    {
                        Title = BrandTitle
                    },
                    Prices = GetPrices(Prices)
                };

                return _Product;
            }

            set
            {
                _Product = value;

                Title = _Product?.Title;
                Code = _Product?.Code;
                BrandTitle = _Product?.Brand?.Title;
                Prices = SetPrices(_Product?.Prices);
            }
        }

        private ObservableCollection<PriceViewModel> SetPrices(ICollection<Price> prices)
        {
            ObservableCollection<PriceViewModel> pricesList = new ObservableCollection<PriceViewModel>();

            if (prices != null)
            {
                foreach (var item in prices)
                {
                    pricesList.Add(new PriceViewModel(_PriceStore)
                    {
                        Price = item
                    });
                }

                var currentPrice = prices.OrderByDescending(x => x.PriceDate).FirstOrDefault();
                CurrentBuyPrice = currentPrice?.Buy.ToString();
                CurrentSellPrice = currentPrice?.Sell.ToString();
            }

            return pricesList;
        }

        private ICollection<Price> GetPrices(ObservableCollection<PriceViewModel> prices)
        {
            List<Price> pricesList = new List<Price>();

            if (RemovedPrices != null)
            {
                foreach (var item in RemovedPrices)
                {
                    pricesList.Add(new Price
                    {
                        Id = item.Id,
                        EntityState = EntityState.Deleted
                    });
                }
            }

            if (prices != null)
            {
                foreach (var item in prices)
                {
                    pricesList.Add(item.Price);
                }
            }

            return pricesList;
        }

        private void Ok()
        {
            _ProductService.Save(Product);
            _ProductStore?.UpdateProduct(this);
            _NavigationService.Close();
        }

        private void Cancel()
        {
            _ProductStore?.UpdateProduct(this);
            _NavigationService.Close();
        }

        private void AddPrice()
        {
            Prices.Add(new PriceViewModel(_PriceStore)
            {
                Buy = decimal.Parse(NewPriceBuy),
                Sell = decimal.Parse(NewPriceSell),
                PriceDate = NewPriceDate
            });
        }

        private void PriceStore_PriceDeleted(PriceViewModel priceViewModel)
        {
            Prices.Remove(priceViewModel);

            if (priceViewModel.Id != 0)
                RemovedPrices.Add(priceViewModel);
        }

        private void View()
        {
            _NavigationService.Navigate(() => this);
        }

        private void Delete(object parameter)
        {
            if (_Product != null)
            {
                try
                {
                    if (bool.Parse(parameter?.ToString()) == true)
                        _ProductService.Delete(_Product.Id);
                    
                    _ProductStore?.DeleteProduct(this);
                }
                catch (DatabaseException ex)
                {
                    string message = ex.Message;
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
        }
    }
}
