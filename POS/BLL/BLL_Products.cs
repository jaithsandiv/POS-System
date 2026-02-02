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
        private readonly BLL_SystemLog _logManager = new BLL_SystemLog();

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
                                 int createdBy, byte[] image = null)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.");
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product code is required.");
            if (unitId <= 0)
                throw new ArgumentException("Unit is required.");
            if (sellingPrice < 0)
                throw new ArgumentException("Selling price cannot be negative.");

            try
            {
                int productId = _dalProducts.InsertProduct(productName, productCode, barcode, productType,
                                                          categoryId, brandId, unitId,
                                                          purchaseCost, sellingPrice, stockQuantity,
                                                          expiryDate, manufactureDate, description,
                                                          createdBy, image);

                _logManager.LogInfo(
                    source: "PRODUCT",
                    message: $"Product created - ID: {productId}, Code: {productCode}, Name: {productName}, Stock: {stockQuantity}, Price: LKR {sellingPrice:N2}",
                    referenceId: productId,
                    userId: createdBy
                );

                return productId;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "PRODUCT",
                    ex: ex,
                    referenceId: null,
                    userId: createdBy
                );
                throw;
            }
        }

        public bool UpdateProduct(int productId, string productName, string productCode, string barcode, string productType,
                                  int? categoryId, int? brandId, int unitId,
                                  decimal? purchaseCost, decimal sellingPrice, decimal stockQuantity,
                                  DateTime? expiryDate, DateTime? manufactureDate, string description,
                                  int updatedBy, byte[] image = null)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name is required.");
            if (string.IsNullOrWhiteSpace(productCode))
                throw new ArgumentException("Product code is required.");
            if (unitId <= 0)
                throw new ArgumentException("Unit is required.");
            if (sellingPrice < 0)
                throw new ArgumentException("Selling price cannot be negative.");

            try
            {
                // Get old values for comparison
                DataTable oldProduct = _dalProducts.GetProductById(productId);
                decimal oldStock = 0;
                decimal oldPrice = 0;

                if (oldProduct.Rows.Count > 0)
                {
                    oldStock = Convert.ToDecimal(oldProduct.Rows[0]["stock_quantity"]);
                    oldPrice = Convert.ToDecimal(oldProduct.Rows[0]["selling_price"]);
                }

                bool success = _dalProducts.UpdateProduct(productId, productName, productCode, barcode, productType,
                                                          categoryId, brandId, unitId,
                                                          purchaseCost, sellingPrice, stockQuantity,
                                                          expiryDate, manufactureDate, description,
                                                          updatedBy, image);

                if (success)
                {
                    string changes = "";
                    if (oldStock != stockQuantity)
                        changes += $" Stock: {oldStock} ? {stockQuantity},";
                    if (oldPrice != sellingPrice)
                        changes += $" Price: LKR {oldPrice:N2} ? LKR {sellingPrice:N2},";

                    _logManager.LogAudit(
                        source: "PRODUCT",
                        message: $"Product updated - ID: {productId}, Code: {productCode}, Name: {productName}{(string.IsNullOrEmpty(changes) ? "" : "," + changes.TrimEnd(','))}",
                        referenceId: productId,
                        userId: updatedBy
                    );

                    // Log low stock warning if applicable
                    if (stockQuantity <= 10 && stockQuantity > 0)
                    {
                        _logManager.LogWarning(
                            source: "STOCK",
                            message: $"Low stock alert - Product: {productName} (ID: {productId}), Current Stock: {stockQuantity}",
                            referenceId: productId,
                            userId: null
                        );
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "PRODUCT",
                    ex: ex,
                    referenceId: productId,
                    userId: updatedBy
                );
                throw;
            }
        }

        public bool DeleteProduct(int productId, int updatedBy)
        {
            try
            {
                // Get product details before deletion
                DataTable product = _dalProducts.GetProductById(productId);
                string productName = product.Rows.Count > 0 ? product.Rows[0]["product_name"].ToString() : "Unknown";
                string productCode = product.Rows.Count > 0 ? product.Rows[0]["product_code"].ToString() : "Unknown";

                bool success = _dalProducts.DeleteProduct(productId, updatedBy);

                if (success)
                {
                    _logManager.LogAudit(
                        source: "PRODUCT",
                        message: $"Product deleted - ID: {productId}, Code: {productCode}, Name: {productName}",
                        referenceId: productId,
                        userId: updatedBy
                    );
                }

                return success;
            }
            catch (Exception ex)
            {
                _logManager.LogError(
                    source: "PRODUCT",
                    ex: ex,
                    referenceId: productId,
                    userId: updatedBy
                );
                throw;
            }
        }

        public DataTable SearchProducts(string keyword)
        {
            return _dalProducts.SearchProducts(keyword);
        }

        /// <summary>
        /// Validates import data columns against required Product table columns
        /// </summary>
        public (bool isValid, string errorMessage, List<string> missingColumns) ValidateImportColumns(DataTable importData)
        {
            var requiredColumns = new List<string> { "product_name", "product_code", "unit_id", "selling_price" };
            var optionalColumns = new List<string> { "barcode", "product_type", "category_id", "brand_id", "purchase_cost", "stock_quantity", "expiry_date", "manufacture_date", "description", "image" };

            var missingRequired = new List<string>();
            foreach (var col in requiredColumns)
            {
                if (!importData.Columns.Contains(col))
                {
                    missingRequired.Add(col);
                }
            }

            if (missingRequired.Count > 0)
            {
                return (false, $"Missing required columns: {string.Join(", ", missingRequired)}", missingRequired);
            }

            return (true, string.Empty, new List<string>());
        }

        /// <summary>
        /// Imports products from a DataTable with full validation
        /// </summary>
        public (int successCount, int failedCount, List<string> errors) ImportProducts(DataTable importData, int createdBy)
        {
            var errors = new List<string>();
            int successCount = 0;
            int failedCount = 0;

            // Validate columns first
            var (isValid, errorMessage, missingColumns) = ValidateImportColumns(importData);
            if (!isValid)
            {
                errors.Add(errorMessage);
                return (0, importData.Rows.Count, errors);
            }

            int rowNumber = 1;
            foreach (DataRow row in importData.Rows)
            {
                rowNumber++;
                try
                {
                    string productName = row["product_name"]?.ToString()?.Trim() ?? "";
                    string productCode = row["product_code"]?.ToString()?.Trim() ?? "";

                    // Validate required fields
                    if (string.IsNullOrWhiteSpace(productName))
                    {
                        errors.Add($"Row {rowNumber}: Product name is required.");
                        failedCount++;
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(productCode))
                    {
                        errors.Add($"Row {rowNumber}: Product code is required.");
                        failedCount++;
                        continue;
                    }

                    // Check for duplicate product code
                    if (_dalProducts.ProductCodeExists(productCode))
                    {
                        errors.Add($"Row {rowNumber}: Product code '{productCode}' already exists.");
                        failedCount++;
                        continue;
                    }

                    int unitId = 0;
                    if (importData.Columns.Contains("unit_id") && row["unit_id"] != DBNull.Value)
                    {
                        int.TryParse(row["unit_id"].ToString(), out unitId);
                    }

                    if (unitId <= 0)
                    {
                        errors.Add($"Row {rowNumber}: Valid unit_id is required.");
                        failedCount++;
                        continue;
                    }

                    decimal sellingPrice = 0;
                    if (importData.Columns.Contains("selling_price") && row["selling_price"] != DBNull.Value)
                    {
                        decimal.TryParse(row["selling_price"].ToString(), out sellingPrice);
                    }

                    if (sellingPrice < 0)
                    {
                        errors.Add($"Row {rowNumber}: Selling price cannot be negative.");
                        failedCount++;
                        continue;
                    }

                    // Extract optional fields
                    string barcode = importData.Columns.Contains("barcode") ? row["barcode"]?.ToString()?.Trim() : null;
                    string productType = importData.Columns.Contains("product_type") ? row["product_type"]?.ToString()?.Trim() : "Standard";

                    int? categoryId = null;
                    if (importData.Columns.Contains("category_id") && row["category_id"] != DBNull.Value)
                    {
                        if (int.TryParse(row["category_id"].ToString(), out int catId))
                            categoryId = catId;
                    }

                    int? brandId = null;
                    if (importData.Columns.Contains("brand_id") && row["brand_id"] != DBNull.Value)
                    {
                        if (int.TryParse(row["brand_id"].ToString(), out int brId))
                            brandId = brId;
                    }

                    decimal? purchaseCost = null;
                    if (importData.Columns.Contains("purchase_cost") && row["purchase_cost"] != DBNull.Value)
                    {
                        if (decimal.TryParse(row["purchase_cost"].ToString(), out decimal pc))
                            purchaseCost = pc;
                    }

                    decimal stockQuantity = 0;
                    if (importData.Columns.Contains("stock_quantity") && row["stock_quantity"] != DBNull.Value)
                    {
                        decimal.TryParse(row["stock_quantity"].ToString(), out stockQuantity);
                    }

                    DateTime? expiryDate = null;
                    if (importData.Columns.Contains("expiry_date") && row["expiry_date"] != DBNull.Value)
                    {
                        if (DateTime.TryParse(row["expiry_date"].ToString(), out DateTime ed))
                            expiryDate = ed;
                    }

                    DateTime? manufactureDate = null;
                    if (importData.Columns.Contains("manufacture_date") && row["manufacture_date"] != DBNull.Value)
                    {
                        if (DateTime.TryParse(row["manufacture_date"].ToString(), out DateTime md))
                            manufactureDate = md;
                    }

                    string description = importData.Columns.Contains("description") ? row["description"]?.ToString()?.Trim() : null;

                    // Image handling
                    byte[] imageBytes = null;
                    if (importData.Columns.Contains("image") && row["image"] != DBNull.Value)
                    {
                        var imgVal = row["image"];
                        if (imgVal is byte[] b)
                        {
                            imageBytes = b;
                        }
                        else
                        {
                            string possiblePath = imgVal.ToString();
                            if (!string.IsNullOrWhiteSpace(possiblePath) && System.IO.File.Exists(possiblePath))
                            {
                                imageBytes = System.IO.File.ReadAllBytes(possiblePath);
                            }
                        }
                    }

                    // Insert product
                    int productId = _dalProducts.InsertProduct(productName, productCode, barcode, productType,
                                                                categoryId, brandId, unitId,
                                                                purchaseCost, sellingPrice, stockQuantity,
                                                                expiryDate, manufactureDate, description,
                                                                createdBy, imageBytes);

                    if (productId > 0)
                    {
                        successCount++;
                        _logManager.LogInfo(
                            source: "PRODUCT_IMPORT",
                            message: $"Product imported - ID: {productId}, Code: {productCode}, Name: {productName}",
                            referenceId: productId,
                            userId: createdBy
                        );
                    }
                    else
                    {
                        errors.Add($"Row {rowNumber}: Failed to insert product '{productName}'.");
                        failedCount++;
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Row {rowNumber}: {ex.Message}");
                    failedCount++;
                }
            }

            if (successCount > 0)
            {
                _logManager.LogAudit(
                    source: "PRODUCT_IMPORT",
                    message: $"Bulk product import completed - Success: {successCount}, Failed: {failedCount}",
                    referenceId: null,
                    userId: createdBy
                );
            }

            return (successCount, failedCount, errors);
        }

        /// <summary>
        /// Checks if a product code already exists
        /// </summary>
        public bool ProductCodeExists(string productCode)
        {
            return _dalProducts.ProductCodeExists(productCode);
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

        /// <summary>
        /// Gets detailed barcode print data including product information for label generation
        /// </summary>
        public DataTable GetBarcodePrintDetails()
        {
            return _dalProducts.GetBarcodePrintDetails();
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
