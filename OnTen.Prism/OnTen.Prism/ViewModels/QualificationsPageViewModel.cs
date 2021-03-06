using OnTen.Common.Responses;
using OnTen.Prism.Helpers;
using OnTen.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnTen.Prism.ViewModels
{
    public class QualificationsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ProductResponse _product;
        private bool _isRunning;
        private ObservableCollection<QualificationItemViewModel> _qualifications;

        public QualificationsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = Languages.Qualifications;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<QualificationItemViewModel> Qualifications
        {
            get => _qualifications;
            set => SetProperty(ref _qualifications, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                IsRunning = true;
                _product = parameters.GetValue<ProductResponse>("product");
                if (_product.Qualifications != null)
                {
                    Qualifications = new ObservableCollection<QualificationItemViewModel>(
                        _product.Qualifications.Select(q => new QualificationItemViewModel(_navigationService)
                        {
                            Date = q.Date,
                            Id = q.Id,
                            Remarks = q.Remarks,
                            Score = q.Score
                        }).ToList());
                }

                IsRunning = false;
            }
        }

    }
}
