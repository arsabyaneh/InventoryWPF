using Inventory.Core.Stores;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class PriceViewModel : BaseViewModel
    {
        private readonly PriceStore _PriceStore;

        private DateTime _PriceDate;
        private decimal _Buy;
        private decimal _Sell;
        private Price _Price;

        public PriceViewModel(PriceStore priceStore)
        {
            _PriceStore = priceStore;

            DeleteCommand = new RelayCommand(Delete);
        }

        public DateTime PriceDate { get => _PriceDate; set => SetProperty(ref _PriceDate, value); }
        public decimal Buy { get => _Buy; set => SetProperty(ref _Buy, value); }
        public decimal Sell { get => _Sell; set => SetProperty(ref _Sell, value); }
        public long Id { get; set; }
        public long ProductId { get; set; }

        public ICommand DeleteCommand { get; }

        public Price Price
        {
            get
            {
                _Price = new Price
                {
                    Id = Id,
                    PriceDate = PriceDate,
                    Buy = Buy,
                    Sell = Sell,
                    ProductId = ProductId
                };

                return _Price;
            }

            set
            {
                _Price = value;

                if (_Price == null)
                    return;

                PriceDate = _Price.PriceDate;
                Buy = _Price.Buy;
                Sell = _Price.Sell;
                Id = _Price.Id;
                ProductId = _Price.ProductId;
            }
        }

        private void Delete()
        {
            _PriceStore?.DeletePrice(this);
        }
    }
}
