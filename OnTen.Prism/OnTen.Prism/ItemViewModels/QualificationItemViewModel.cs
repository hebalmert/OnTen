using OnTen.Common.Responses;
using OnTen.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTen.Prism.ItemViewModels
{
    public class QualificationItemViewModel : QualificationResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectQualificationCommand;

        public QualificationItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectQualificationCommand => _selectQualificationCommand ?? (_selectQualificationCommand = new DelegateCommand(SelectQualificationAsync));

        private async void SelectQualificationAsync()
        {
            NavigationParameters parameters = new NavigationParameters
        {
            { "qualification", this }
        };

            await _navigationService.NavigateAsync(nameof(QualificationDetailPage), parameters);
        }

    }
}
