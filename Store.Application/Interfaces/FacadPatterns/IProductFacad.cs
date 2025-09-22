using Store.Application.Services.Products.commands.EditCategory;
using Store.Application.Services.Products.commands.EditProduct;
using Store.Application.Services.Products.commands.EditProductFeature;
using Store.Application.Services.Products.commands.RemoveCategory;
using Store.Application.Services.Products.commands.RemoveProduct;
using Store.Application.Services.Products.commands.RemoveProductFeature;
using Store.Application.Services.Products.commands.RemoveProductImage;
using Store.Application.Services.Products.Commands.AddNewCategory;
using Store.Application.Services.Products.Commands.AddNewProduct;
using Store.Application.Services.Products.Commands.AddNewProductFeature;
using Store.Application.Services.Products.Commands.AddNewProductImage;
using Store.Application.Services.Products.Queries.GetAllCategories;
using Store.Application.Services.Products.Queries.GetCategories;
using Store.Application.Services.Products.Queries.GetProductDetailForAdmin;
using Store.Application.Services.Products.Queries.GetProductDetailForSite;
using Store.Application.Services.Products.Queries.GetProductForAdmin;
using Store.Application.Services.Products.Queries.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        AddNewCategoryService AddNewCategoryService { get; }
        IGetCategoriesService  GetCategoriesService { get; }
        AddNewProductService AddNewProductService { get; }
        IGetAllCategoriesService GetAllCategoriesService { get; }
        IGetProductForAdminService GetProductForAdminService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IGetProductForSiteService GetProductForSiteService { get; }
        IGetProductDetailForSiteService GetProductDetailForSiteService { get; }
        IRemoveProductService RemoveProductService { get; }
        IRemoveCategoryService RemoveCategoryService { get; }
        IEditCategoryService EditCategoryService { get; }
        IEditProductService EditProductService { get; }
        IEditProductFeatureService EditProductFeatureService { get; }
        IRemoveProductFeatureService RemoveProductFeatureService { get; }
        IAddNewProductFeatureService AddNewProductFeatureService { get; }
        IAddNewProductImageService AddNewProductImageService { get; }
        IRemoveProductImageService RemoveProductImageService { get; }
    }
}
