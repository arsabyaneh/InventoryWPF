using Inventory.Core.ViewModels;
using Inventory.Core.ViewModels.Base;
using System;

namespace Inventory.Core.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly Func<NavigationBarViewModel> _createNavigationBarViewModel;
        private readonly Func<LoginViewModel> _createLoginViewModel;
        private readonly Func<MessageViewModel> _createMessageViewModel;
        private readonly Func<CustomerViewModel> _createCustomerViewModel;
        private readonly Func<EmployeeViewModel> _createEmployeeViewModel;
        private readonly Func<HomeViewModel> _createHomeViewModel;
        private readonly Func<InvoiceViewModel> _createInvoiceViewModel;
        private readonly Func<InvoiceItemViewModel> _createInvoiceItemViewModel;
        private readonly Func<InvoicesListViewModel> _createInvoicesListViewModel;
        private readonly Func<ListPageViewModel> _createListPageViewModel;
        private readonly Func<PriceViewModel> _createPriceViewModel;
        private readonly Func<ProductViewModel> _createProductViewModel;
        private readonly Func<ProductsListViewModel> _createProductsListViewModel;
        private readonly Func<RoleViewModel> _createRoleViewModel;

        public ViewModelFactory(Func<NavigationBarViewModel> createNavigationBarViewModel, Func<LoginViewModel> createLoginViewModel,
            Func<MessageViewModel> createMessageViewModel, Func<CustomerViewModel> createCustomerViewModel,
            Func<EmployeeViewModel> createEmployeeViewModel, Func<HomeViewModel> createHomeViewModel,
            Func<InvoiceViewModel> createInvoiceViewModel, Func<InvoiceItemViewModel> createInvoiceItemViewModel,
            Func<InvoicesListViewModel> createInvoicesListViewModel, Func<ListPageViewModel> createListPageViewModel,
            Func<PriceViewModel> createPriceViewModel, Func<ProductViewModel> createProductViewModel,
            Func<ProductsListViewModel> createProductsListViewModel, Func<RoleViewModel> createRoleViewModel)
        {
            _createNavigationBarViewModel = createNavigationBarViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createMessageViewModel = createMessageViewModel;
            _createCustomerViewModel = createCustomerViewModel;
            _createEmployeeViewModel = createEmployeeViewModel;
            _createHomeViewModel = createHomeViewModel;
            _createInvoiceViewModel = createInvoiceViewModel;
            _createInvoiceItemViewModel = createInvoiceItemViewModel;
            _createInvoicesListViewModel = createInvoicesListViewModel;
            _createListPageViewModel = createListPageViewModel;
            _createPriceViewModel = createPriceViewModel;
            _createProductViewModel = createProductViewModel;
            _createProductsListViewModel = createProductsListViewModel;
            _createRoleViewModel = createRoleViewModel;
        }

        public BaseViewModel? CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home: return _createHomeViewModel();

                case ViewType.NavigationBar: return _createNavigationBarViewModel();

                case ViewType.Login: return _createLoginViewModel();

                case ViewType.Message: return _createMessageViewModel();

                case ViewType.Customer: return _createCustomerViewModel();

                case ViewType.Employee: return _createEmployeeViewModel();

                case ViewType.Invoice: return _createInvoiceViewModel();

                case ViewType.InvoiceItem: return _createInvoiceItemViewModel();

                case ViewType.InvoicesList: return _createInvoicesListViewModel();

                case ViewType.ListPage: return _createListPageViewModel();

                case ViewType.Price: return _createPriceViewModel();

                case ViewType.Product: return _createProductViewModel();

                case ViewType.ProductsList: return _createProductsListViewModel();

                case ViewType.Role: return _createRoleViewModel();

                default: return null;
            }
        }
    }
}
