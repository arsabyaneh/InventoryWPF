using Inventory.Core.Services;
using Inventory.Core.Stores;
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
        private readonly ProductStore _ProductStore;
        private readonly Func<ProductStore> _CreateProductStore;

        private ObservableCollection<ProductViewModel> _ProductViewModels;
        private ListPageViewModel _ListPageViewModel;

        private string _Title;

        public ProductsListViewModel(INavigationService navigationService, INavigationService modalNavigationService, IProductService productService, Func<ProductStore> createProductStore)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _ProductService = productService;
            _CreateProductStore = createProductStore;
            _ProductStore = _CreateProductStore();

            _ProductStore.ProductDeleted += ProductStore_ProductDeleted;

            _ListPageViewModel = new ListPageViewModel();
            _ListPageViewModel.PageChanged += ListPageViewModel_PageChanged;
            _ListPageViewModel.TotalNumberOfRecords = _ProductService.GetTotalNumberOfProductRecordsInDatabase();

            ProductViewModels = GetProductViewModels(_ProductService.LoadProductsFromDatabase((_ListPageViewModel.CurrentPageNumber - 1) * _ListPageViewModel.RowsPerPage, _ListPageViewModel.RowsPerPage));
        }

        public ProductViewModel SelectedProductViewModel { get; set; }
        public ObservableCollection<ProductViewModel> ProductViewModels { get => _ProductViewModels; set => SetProperty(ref _ProductViewModels, value); }
        public ListPageViewModel ListPageViewModel { get => _ListPageViewModel; set => SetProperty(ref _ListPageViewModel, value); }
        public string Title { get => _Title; set => SetProperty(ref _Title, value); }

        private ObservableCollection<ProductViewModel> GetProductViewModels(IEnumerable<Product> products)
        {
            ObservableCollection<ProductViewModel> productViewModels = new ObservableCollection<ProductViewModel>();

            if (products != null)
            {
                foreach (var item in products)
                {
                    productViewModels.Add(new ProductViewModel(_ModalNavigationService, _ProductService, _ProductStore, item));
                }
            }

            return productViewModels;
        }

        private async void ListPageViewModel_PageChanged()
        {
            ProductViewModels = GetProductViewModels(await _ProductService.LoadProductsFromDatabase((ListPageViewModel.CurrentPageNumber - 1) * ListPageViewModel.RowsPerPage, ListPageViewModel.RowsPerPage, Title));
            SelectedProductViewModel = null;
        }

        private async void ProductStore_ProductDeleted(ProductViewModel obj)
        {
            ListPageViewModel.CurrentPageNumber = 1;
            ListPageViewModel.TotalNumberOfRecords = await _ProductService.GetTotalNumberOfProductRecordsInDatabase(Title);
            ProductViewModels = GetProductViewModels(await _ProductService.LoadProductsFromDatabase((ListPageViewModel.CurrentPageNumber - 1) * ListPageViewModel.RowsPerPage, ListPageViewModel.RowsPerPage, Title));
            SelectedProductViewModel = null;
        }
    }
}
