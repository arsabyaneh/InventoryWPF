using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Inventory.Core.ViewModels
{
    public class ListPageViewModel : BaseViewModel
    {
        public int RowsPerPage = 15;

        private long _CurrentPageNumber = 1;
        private long _TotalNumberOfPages;
        private long _TotalNumberOfRecords;

        public ListPageViewModel()
        {
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
        }

        public long CurrentPageNumber { get => _CurrentPageNumber; set=>SetProperty(ref _CurrentPageNumber,value); }
        public long TotalNumberOfPages { get => _TotalNumberOfPages; set=>SetProperty(ref _TotalNumberOfPages,value); }
        public long TotalNumberOfRecords
        {
            get => _TotalNumberOfRecords;
            set
            {
                _TotalNumberOfRecords = value;
                TotalNumberOfPages = _TotalNumberOfRecords == 0 ? 1 : (_TotalNumberOfRecords / RowsPerPage) + (_TotalNumberOfRecords % RowsPerPage == 0 ? 0 : 1);
            }
        }

        public event Action? PageChanged;

        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }

        private void NextPage()
        {
            if (CurrentPageNumber < TotalNumberOfPages)
            {
                CurrentPageNumber++;
                PageChanged?.Invoke();
            }
        }

        private void PreviousPage()
        {
            if (CurrentPageNumber > 1)
            {
                CurrentPageNumber--;
                PageChanged?.Invoke();
            }
        }
    }
}
