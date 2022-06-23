using OnTen.Common.Entities;
using OnTen.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OnTen.Prism.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        private Product _product;
        private ObservableCollection<ProductImage> _images;

        public ProductDetailPageViewModel( INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Detalle de Producto";
        }

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        public ObservableCollection<ProductImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<Product>("product");
                Title = Languages.Product;
                Images = new ObservableCollection<ProductImage>(Product.ProductImages);
            }
        }

    }
}
