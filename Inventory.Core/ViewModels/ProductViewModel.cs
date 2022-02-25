using Inventory.Core.Services;
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

        private string _Title;
        private string _Code;
        private string _BrandTitle;
        private string _NewPriceBuy;
        private string _NewPriceSell;
        private DateTime _NewPriceDate = DateTime.Now;
        private string _CurrentSellPrice;
        private string _CurrentBuyPrice;
        private ObservableCollection<Price> _Prices = new ObservableCollection<Price>();
        private Product _Product;

        public ProductViewModel(INavigationService navigationService, IProductService productService, Product product)
        {
            _NavigationService = navigationService;
            _ProductService = productService;

            Product = product;

            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            AddPriceCommand = new RelayCommand(AddPrice);
            ViewCommand = new RelayCommand(View);
        }

        public string Title { get => _Title; set => SetProperty(ref _Title, value); }
        public string Code { get => _Code; set => SetProperty(ref _Code, value); }
        public string BrandTitle { get => _BrandTitle; set => SetProperty(ref _BrandTitle, value); }
        public string NewPriceBuy { get => _NewPriceBuy; set => SetProperty(ref _NewPriceBuy, value); }
        public string NewPriceSell { get => _NewPriceSell; set=>SetProperty(ref _NewPriceSell, value); }
        public DateTime NewPriceDate { get => _NewPriceDate; set=>SetProperty(ref _NewPriceDate, value); }
        public string CurrentSellPrice { get => _CurrentSellPrice; set => SetProperty(ref _CurrentSellPrice, value); }
        public string CurrentBuyPrice { get => _CurrentBuyPrice; set => SetProperty(ref _CurrentBuyPrice, value); }
        public ObservableCollection<Price> Prices { get => _Prices; set => SetProperty(ref _Prices, value); }

        private ObservableCollection<Price> RemovedPrices { get; set; }

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

        private ObservableCollection<Price> SetPrices(ICollection<Price> prices)
        {
            ObservableCollection<Price> pricesList = new ObservableCollection<Price>();

            if (prices != null)
            {
                foreach (var item in prices)
                {
                    pricesList.Add(new Price
                    {
                        Id = item.Id,
                        Buy = item.Buy,
                        Sell = item.Sell,
                        PriceDate = item.PriceDate,
                        ProductId = item.ProductId
                    });
                }

                var currentPrice = prices.OrderByDescending(x => x.PriceDate).FirstOrDefault();
                CurrentBuyPrice = currentPrice?.Buy.ToString();
                CurrentSellPrice = currentPrice?.Sell.ToString();
            }

            return pricesList;
        }

        private ICollection<Price> GetPrices(ObservableCollection<Price> prices)
        {
            List<Price> pricesList = new List<Price>(prices);

            if (RemovedPrices != null)
            {
                foreach (var item in RemovedPrices)
                {
                    pricesList.Add(item);
                }
            }

            return pricesList;
        }

        private void Ok()
        {
            _ProductService.Save(Product);
            _NavigationService.Close();
        }

        private void Cancel()
        {
            _NavigationService.Close();
        }

        private void AddPrice()
        {
            Prices.Add(new Price
            {
                Buy = decimal.Parse(NewPriceBuy),
                Sell = decimal.Parse(NewPriceSell),
                PriceDate = NewPriceDate,
                EntityState = EntityState.Added
            });
        }

        private void View()
        {
            _NavigationService.Navigate(() => this);
        }
    }
}
