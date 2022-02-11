using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly INavigationService _ModalNavigationService;
        private readonly IProductService _ProductService;

        public HomeViewModel(INavigationService navigationService, INavigationService modalNavigationService, IProductService productService)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _ProductService = productService;

            AddProductCommand = new RelayCommand(AddProduct);
        }

        public ICommand AddProductCommand { get; }

        private void AddProduct()
        {
            _ModalNavigationService.Navigate(() => new ProductViewModel(_ModalNavigationService, _ProductService)
            {
                ControlWidth = 700
            });
        }
    }
}
