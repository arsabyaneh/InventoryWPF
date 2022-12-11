using Inventory.Core.Factories;
using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.Core.ViewModels.Base;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Inventory.Core.ViewModels
{
    public class ProductsListViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly INavigationService _ModalNavigationService;
        private readonly IProductService _ProductService;
        private readonly ProductStore _ProductStore;
        private readonly Func<ProductStore> _CreateProductStore;
        private readonly IViewModelFactory _ViewModelFactory;

        private ObservableCollection<ProductViewModel> _ProductViewModels;
        private ListPageViewModel _ListPageViewModel;

        private string _Title;

        public ProductsListViewModel(INavigationService navigationService, INavigationService modalNavigationService, 
            IProductService productService, Func<ProductStore> createProductStore, IViewModelFactory viewModelFactory)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _ProductService = productService;
            _CreateProductStore = createProductStore;
            _ProductStore = _CreateProductStore();
            _ViewModelFactory = viewModelFactory;

            _ProductStore.ProductDeleted += ProductStore_ProductDeleted;
            _ProductStore.ProductUpdated += ProductStore_ProductUpdated;

            if (_ViewModelFactory.CreateViewModel(ViewType.ListPage) is ListPageViewModel listPageViewModel)
            {
                _ListPageViewModel = listPageViewModel;
                _ListPageViewModel.PageChanged += ListPageViewModel_PageChanged;
                _ListPageViewModel.TotalNumberOfRecords = _ProductService.GetTotalNumberOfProductRecordsInDatabase();

                ProductViewModels = GetProductViewModels(_ProductService.LoadProductsFromDatabase((_ListPageViewModel.CurrentPageNumber - 1) * _ListPageViewModel.RowsPerPage, _ListPageViewModel.RowsPerPage));
            }
        }

        public ProductViewModel? SelectedProductViewModel { get; set; }
        public ObservableCollection<ProductViewModel> ProductViewModels { get => _ProductViewModels; set => SetProperty(ref _ProductViewModels, value); }
        public ListPageViewModel ListPageViewModel { get => _ListPageViewModel; set => SetProperty(ref _ListPageViewModel, value); }
        public string Title { get => _Title; set => SetProperty(ref _Title, value); }

        private ObservableCollection<ProductViewModel> GetProductViewModels(IEnumerable<Product> products)
        {
            var productViewModels = new ObservableCollection<ProductViewModel>();

            if (products != null)
            {
                foreach (var item in products)
                {
                    if (_ViewModelFactory.CreateViewModel(ViewType.Product) is ProductViewModel productViewModel)
                    {
                        productViewModel.Initialise(_ProductStore, Microsoft.EntityFrameworkCore.EntityState.Modified, item);
                        productViewModel.ControlWidth = 700;
                        productViewModels.Add(productViewModel);
                    }
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

        private void ProductStore_ProductUpdated(ProductViewModel obj)
        {
            ListPageViewModel_PageChanged();
        }
    }
}
