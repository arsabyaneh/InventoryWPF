using Inventory.Core.Services;
using Inventory.Core.Stores;
using Inventory.EntityFramework.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.ViewModels
{
    public class InvoicesListViewModel : BaseViewModel
    {
        private readonly INavigationService _NavigationService;
        private readonly INavigationService _ModalNavigationService;
        private readonly IInvoiceService _InvoiceService;
        private readonly IProductService _ProductService;
        private readonly ICustomerService _CustomerService;
        private readonly AccountStore _AccountStore;
        private readonly InvoiceStore _InvoiceStore;
        private readonly Func<InvoiceItemStore> _CreateInvoiceItemStore;

        private ObservableCollection<InvoiceViewModel> _InvoiceViewModels;
        private ListPageViewModel _ListPageViewModel;
        private string _Title;

        public InvoicesListViewModel(INavigationService navigationService, INavigationService modalNavigationService, IInvoiceService invoiceService, IProductService productService, ICustomerService customerService, 
            AccountStore accountStore, InvoiceStore invoiceStore, Func<InvoiceItemStore> createInvoiceItemStore)
        {
            _NavigationService = navigationService;
            _ModalNavigationService = modalNavigationService;
            _InvoiceService = invoiceService;
            _ProductService = productService;
            _CustomerService = customerService;
            _AccountStore = accountStore;
            _InvoiceStore = invoiceStore;
            _CreateInvoiceItemStore = createInvoiceItemStore;

            _InvoiceStore.InvoiceDeleted += InvoiceStore_InvoiceDeleted;
            _InvoiceStore.InvoiceUpdated += InvoiceStore_InvoiceUpdated;

            _ListPageViewModel = new ListPageViewModel();
            _ListPageViewModel.PageChanged += ListPageViewModel_PageChanged;
            _ListPageViewModel.TotalNumberOfRecords = _InvoiceService.GetTotalNumberOfInvoiceRecordsInDatabase();

            InvoiceViewModels = GetInvoiceViewModels(_InvoiceService.LoadInvoicesFromDatabase((_ListPageViewModel.CurrentPageNumber - 1) * _ListPageViewModel.RowsPerPage, _ListPageViewModel.RowsPerPage));
        }

        public InvoiceViewModel SelectedInvoiceViewModel { get; set; }
        public ObservableCollection<InvoiceViewModel> InvoiceViewModels { get => _InvoiceViewModels; set => SetProperty(ref _InvoiceViewModels, value); }
        public ListPageViewModel ListPageViewModel { get => _ListPageViewModel; set => SetProperty(ref _ListPageViewModel, value); }
        public string Title { get => _Title; set => SetProperty(ref _Title, value); }

        private ObservableCollection<InvoiceViewModel> GetInvoiceViewModels(IEnumerable<Invoice> invoices)
        {
            ObservableCollection<InvoiceViewModel> invoiceViewModels = new ObservableCollection<InvoiceViewModel>();

            if (invoices != null)
            {
                foreach (var item in invoices)
                {
                    invoiceViewModels.Add(new InvoiceViewModel(_ModalNavigationService, _InvoiceService, _ProductService, _CustomerService, _AccountStore, _InvoiceStore, _CreateInvoiceItemStore(), item));
                }
            }

            return invoiceViewModels;
        }

        private void ListPageViewModel_PageChanged()
        {
            InvoiceViewModels = GetInvoiceViewModels(_InvoiceService.LoadInvoicesFromDatabase((ListPageViewModel.CurrentPageNumber - 1) * ListPageViewModel.RowsPerPage, ListPageViewModel.RowsPerPage));
            SelectedInvoiceViewModel = null;
        }

        private async void InvoiceStore_InvoiceDeleted(InvoiceViewModel obj)
        {
            ListPageViewModel.CurrentPageNumber = 1;
            ListPageViewModel.TotalNumberOfRecords = await _InvoiceService.GetTotalNumberOfInvoiceRecordsInDatabase(Title);
            InvoiceViewModels = GetInvoiceViewModels(await _InvoiceService.LoadInvoicesFromDatabase((ListPageViewModel.CurrentPageNumber - 1) * ListPageViewModel.RowsPerPage, ListPageViewModel.RowsPerPage, Title));
            SelectedInvoiceViewModel = null;
        }

        private void InvoiceStore_InvoiceUpdated(InvoiceViewModel invoiceViewModel)
        {
            ListPageViewModel_PageChanged();
        }
    }
}
