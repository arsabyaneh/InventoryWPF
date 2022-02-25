using Inventory.Core.Services;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class ProductsListViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly INavigationService _ModalNavigationService;
        private readonly IProductService _ProductService;

        private ObservableCollection<ProductViewModel> _ProductViewModels;
        private ListPageViewModel _ListPageViewModel;

        public ProductsListViewModel(INavigationService navigationService, INavigationService modalNavigationService, IProductService productService)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _ProductService = productService;

            _ListPageViewModel = new ListPageViewModel();
            _ListPageViewModel.PageChanged += ListPageViewModel_PageChanged;
            _ListPageViewModel.TotalNumberOfRecords = _ProductService.GetTotalNumberOfProductRecordsInDatabase();

            ProductViewModels = GetProductViewModels(_ProductService.LoadProductsFromDatabase((_ListPageViewModel.CurrentPageNumber - 1) * _ListPageViewModel.RowsPerPage, _ListPageViewModel.RowsPerPage));
        }

        public ProductViewModel SelectedProductViewModel { get; set; }
        public ObservableCollection<ProductViewModel> ProductViewModels { get => _ProductViewModels; set=>SetProperty(ref _ProductViewModels, value); }
        public ListPageViewModel ListPageViewModel { get => _ListPageViewModel; set =>SetProperty(ref _ListPageViewModel, value); }

        private ObservableCollection<ProductViewModel> GetProductViewModels(IEnumerable<Product> products)
        {
            ObservableCollection<ProductViewModel> productViewModels = new ObservableCollection<ProductViewModel>();

            if (products != null)
            {
                foreach (var item in products)
                {
                    productViewModels.Add(new ProductViewModel(_ModalNavigationService, _ProductService, item));
                }
            }

            return productViewModels;
        }

        private void ListPageViewModel_PageChanged()
        {
            ProductViewModels = GetProductViewModels(_ProductService.LoadProductsFromDatabase((ListPageViewModel.CurrentPageNumber - 1) * ListPageViewModel.RowsPerPage, ListPageViewModel.RowsPerPage));
            SelectedProductViewModel = null;
        }
    }
}
