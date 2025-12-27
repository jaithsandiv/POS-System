using POS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.BLL
{
    internal class BLL_Products
    {
        private readonly DAL_Products _dalProducts = new DAL_Products();

        #region Category

        public DataTable GetCategories()
        {
            return _dalProducts.GetCategories();
        }

        public DataTable GetCategoryById(int categoryId)
        {
            return _dalProducts.GetCategoryById(categoryId);
        }

        public int InsertCategory(string categoryName, int createdBy)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name is required.");

            return _dalProducts.InsertCategory(categoryName, createdBy);
        }

        public bool UpdateCategory(int categoryId, string categoryName, int updatedBy)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                throw new ArgumentException("Category name is required.");

            return _dalProducts.UpdateCategory(categoryId, categoryName, updatedBy);
        }

        public bool DeleteCategory(int categoryId, int updatedBy)
        {
            return _dalProducts.DeleteCategory(categoryId, updatedBy);
        }

        public DataTable SearchCategories(string keyword)
        {
            return _dalProducts.SearchCategories(keyword);
        }

        #endregion

        #region Brand

        public DataTable GetBrands()
        {
            return _dalProducts.GetBrands();
        }

        public DataTable GetBrandById(int brandId)
        {
            return _dalProducts.GetBrandById(brandId);
        }

        public int InsertBrand(string brandName, int? supplierId, int createdBy)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                throw new ArgumentException("Brand name is required.");

            return _dalProducts.InsertBrand(brandName, supplierId, createdBy);
        }

        public bool UpdateBrand(int brandId, string brandName, int? supplierId, int updatedBy)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                throw new ArgumentException("Brand name is required.");

            return _dalProducts.UpdateBrand(brandId, brandName, supplierId, updatedBy);
        }

        public bool DeleteBrand(int brandId, int updatedBy)
        {
            return _dalProducts.DeleteBrand(brandId, updatedBy);
        }

        public DataTable SearchBrands(string keyword)
        {
            return _dalProducts.SearchBrands(keyword);
        }

        #endregion

        #region Unit

        public DataTable GetUnits()
        {
            return _dalProducts.GetUnits();
        }

        public DataTable GetUnitById(int unitId)
        {
            return _dalProducts.GetUnitById(unitId);
        }

        public int InsertUnit(string code, string name, int createdBy)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Unit code is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Unit name is required.");

            return _dalProducts.InsertUnit(code, name, createdBy);
        }

        public bool UpdateUnit(int unitId, string code, string name, int updatedBy)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Unit code is required.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Unit name is required.");

            return _dalProducts.UpdateUnit(unitId, code, name, updatedBy);
        }

        public bool DeleteUnit(int unitId, int updatedBy)
        {
            return _dalProducts.DeleteUnit(unitId, updatedBy);
        }

        public DataTable SearchUnits(string keyword)
        {
            return _dalProducts.SearchUnits(keyword);
        }

        #endregion

        #region Product

        public DataTable GetProducts()
        {
            return _dalProducts.GetProducts();
        }

        public DataTable GetProductById(int productId)
        {
            return _dalProducts.GetProductById(productId);
        }

        public int InsertProduct(string productName, string productCode, string barcode, string productType,
                                 int? categoryId, int? brandId, int unitId,
                                 decimal? purchaseCost, decimal sellingPrice, decimal stockQuantity,
                                 DateTime? expiryDate, DateTime? manufactureDate, string description,
                                 int createdBy)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.");
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product code is required.");
            if (unitId <= 0)
                throw new ArgumentException("Unit is required.");
            if (sellingPrice < 0)
                throw new ArgumentException("Selling price cannot be negative.");

            return _dalProducts.InsertProduct(productName, productCode, barcode, productType,
                                              categoryId, brandId, unitId,
                                              purchaseCost, sellingPrice, stockQuantity,
                                              expiryDate, manufactureDate, description,
                                              createdBy);
        }

        public bool UpdateProduct(int productId, string productName, string productCode, string barcode, string productType,
                                  int? categoryId, int? brandId, int unitId,
                                  decimal? purchaseCost, decimal sellingPrice, decimal stockQuantity,
                                  DateTime? expiryDate, DateTime? manufactureDate, string description,
                                  int updatedBy)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.");
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product code is required.");
            if (unitId <= 0)
                throw new ArgumentException("Unit is required.");
            if (sellingPrice < 0)
                throw new ArgumentException("Selling price cannot be negative.");

            return _dalProducts.UpdateProduct(productId, productName, productCode, barcode, productType,
                                              categoryId, brandId, unitId,
                                              purchaseCost, sellingPrice, stockQuantity,
                                              expiryDate, manufactureDate, description,
                                              updatedBy);
        }

        public bool DeleteProduct(int productId, int updatedBy)
        {
            return _dalProducts.DeleteProduct(productId, updatedBy);
        }

        public DataTable SearchProducts(string keyword)
        {
            return _dalProducts.SearchProducts(keyword);
        }

        #endregion

        #region Barcode Print

        public DataTable GetBarcodePrints()
        {
            return _dalProducts.GetBarcodePrints();
        }

        public DataTable SearchBarcodePrints(string keyword)
        {
            return _dalProducts.SearchBarcodePrints(keyword);
        }

        public int InsertBarcodePrint(int productId, int quantity, bool includeName, bool includePrice,
                                      bool includeExpiry, bool includeManufacture, bool includePromo, int createdBy)
        {
            if (productId <= 0)
                throw new ArgumentException("Product is required for printing.");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            return _dalProducts.InsertBarcodePrint(productId, quantity, includeName, includePrice,
                                                   includeExpiry, includeManufacture, includePromo, createdBy);
        }

        public void ClearBarcodePrints()
        {
            _dalProducts.ClearBarcodePrints();
        }

        #endregion

        #region Stock Report

        /// <summary>
        /// Gets comprehensive stock report for all products
        /// </summary>
        public DataTable GetStockReport()
        {
            return _dalProducts.GetStockReport();
        }

        /// <summary>
        /// Searches stock report by keyword
        /// </summary>
        public DataTable SearchStockReport(string keyword)
        {
            return _dalProducts.SearchStockReport(keyword);
        }

        /// <summary>
        /// Gets detailed stock history for a specific product
        /// </summary>
        public DataTable GetProductStockHistory(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID.");

            return _dalProducts.GetProductStockHistory(productId);
        }

        #endregion
    }
}
