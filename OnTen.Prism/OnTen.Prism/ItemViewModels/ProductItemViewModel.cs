using OnTen.Common.Responses;
using OnTen.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnTen.Prism.ItemViewModels
{
    public class ProductItemViewModel : ProductResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectProductCommand;

        public ProductItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectProductCommand => _selectProductCommand ?? (_selectProductCommand = new DelegateCommand(SelectProductAsync));

        private async void SelectProductAsync()
        {
            //await _navigationService.NavigateAsync(nameof(ProductDetailPage));

            NavigationParameters parameters = new NavigationParameters
            {
                { "product", this }
            };

            await _navigationService.NavigateAsync(nameof(ProductTabbedPage), parameters);
        }

    }
}
