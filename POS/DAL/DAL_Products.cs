using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.DAL
{
    internal class DAL_Products
    {
        public DAL_Products()
        {
        }

        #region Category Methods

        public DataTable GetCategories()
        {
            DataTable dt = new DataTable("Category");
            dt.Columns.Add("category_id", typeof(int));
            dt.Columns.Add("category_name", typeof(string));
            dt.Columns.Add("status", typeof(string));
            dt.Columns.Add("created_by", typeof(int));
            dt.Columns.Add("created_date", typeof(DateTime));
            dt.Columns.Add("updated_by", typeof(int));
            dt.Columns.Add("updated_date", typeof(DateTime));

            string query = @"
                SELECT * FROM Category
                WHERE status = 'A'
                ORDER BY category_name";

            DataTable result = Connection.ExecuteQuery(query);
            return result;
        }

        public DataTable GetCategoryById(int categoryId)
        {
             string query = "SELECT * FROM Category WHERE category_id = @category_id AND status = 'A'";
             SqlParameter[] parameters = { new SqlParameter("@category_id", categoryId) };
             return Connection.ExecuteQuery(query, parameters);
        }

        public int InsertCategory(string categoryName, int createdBy)
        {
            string query = @"
                INSERT INTO Category (category_name, status, created_by, created_date)
                VALUES (@category_name, 'A', @created_by, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            SqlParameter[] parameters = {
                new SqlParameter("@category_name", categoryName),
                new SqlParameter("@created_by", createdBy)
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
                return Convert.ToInt32(result.Rows[0][0]);

            throw new Exception("Failed to insert Category.");
        }

        public bool UpdateCategory(int categoryId, string categoryName, int updatedBy)
        {
            string query = @"
                UPDATE Category
                SET category_name = @category_name,
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE category_id = @category_id AND status = 'A'";

            SqlParameter[] parameters = {
                new SqlParameter("@category_id", categoryId),
                new SqlParameter("@category_name", categoryName),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteCategory(int categoryId, int updatedBy)
        {
            string query = @"
                UPDATE Category
                SET status = 'I',
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE category_id = @category_id";

            SqlParameter[] parameters = {
                new SqlParameter("@category_id", categoryId),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable SearchCategories(string keyword)
        {
            string query = @"
                SELECT * FROM Category
                WHERE status = 'A' AND (
                    category_name LIKE @keyword
                )
                ORDER BY category_name";

            SqlParameter[] parameters = { new SqlParameter("@keyword", "%" + keyword + "%") };
            return Connection.ExecuteQuery(query, parameters);
        }

        #endregion

        #region Brand Methods

        public DataTable GetBrands()
        {
            string query = @"
                SELECT b.*, s.supplier_name
                FROM Brand b
                LEFT JOIN Supplier s ON b.supplier_id = s.supplier_id
                WHERE b.status = 'A'
                ORDER BY b.brand_name";

            return Connection.ExecuteQuery(query);
        }

        public DataTable GetBrandById(int brandId)
        {
            string query = "SELECT * FROM Brand WHERE brand_id = @brand_id AND status = 'A'";
            SqlParameter[] parameters = { new SqlParameter("@brand_id", brandId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        public int InsertBrand(string brandName, int? supplierId, int createdBy)
        {
            string query = @"
                INSERT INTO Brand (brand_name, supplier_id, status, created_by, created_date)
                VALUES (@brand_name, @supplier_id, 'A', @created_by, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            SqlParameter[] parameters = {
                new SqlParameter("@brand_name", brandName),
                new SqlParameter("@supplier_id", supplierId.HasValue ? (object)supplierId.Value : DBNull.Value),
                new SqlParameter("@created_by", createdBy)
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
                return Convert.ToInt32(result.Rows[0][0]);

            throw new Exception("Failed to insert Brand.");
        }

        public bool UpdateBrand(int brandId, string brandName, int? supplierId, int updatedBy)
        {
            string query = @"
                UPDATE Brand
                SET brand_name = @brand_name,
                    supplier_id = @supplier_id,
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE brand_id = @brand_id AND status = 'A'";

            SqlParameter[] parameters = {
                new SqlParameter("@brand_id", brandId),
                new SqlParameter("@brand_name", brandName),
                new SqlParameter("@supplier_id", supplierId.HasValue ? (object)supplierId.Value : DBNull.Value),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteBrand(int brandId, int updatedBy)
        {
            string query = @"
                UPDATE Brand
                SET status = 'I',
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE brand_id = @brand_id";

            SqlParameter[] parameters = {
                new SqlParameter("@brand_id", brandId),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable SearchBrands(string keyword)
        {
            string query = @"
                SELECT b.*, s.supplier_name
                FROM Brand b
                LEFT JOIN Supplier s ON b.supplier_id = s.supplier_id
                WHERE b.status = 'A' AND (
                    b.brand_name LIKE @keyword OR
                    s.supplier_name LIKE @keyword
                )
                ORDER BY b.brand_name";

            SqlParameter[] parameters = { new SqlParameter("@keyword", "%" + keyword + "%") };
            return Connection.ExecuteQuery(query, parameters);
        }

        #endregion

        #region Unit Methods

        public DataTable GetUnits()
        {
            string query = "SELECT * FROM Unit WHERE status = 'A' ORDER BY name";
            return Connection.ExecuteQuery(query);
        }

        public DataTable GetUnitById(int unitId)
        {
            string query = "SELECT * FROM Unit WHERE unit_id = @unit_id AND status = 'A'";
            SqlParameter[] parameters = { new SqlParameter("@unit_id", unitId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        public int InsertUnit(string code, string name, int createdBy)
        {
            string query = @"
                INSERT INTO Unit (code, name, status, created_by, created_date)
                VALUES (@code, @name, 'A', @created_by, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            SqlParameter[] parameters = {
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@created_by", createdBy)
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
                return Convert.ToInt32(result.Rows[0][0]);

            throw new Exception("Failed to insert Unit.");
        }

        public bool UpdateUnit(int unitId, string code, string name, int updatedBy)
        {
            string query = @"
                UPDATE Unit
                SET code = @code,
                    name = @name,
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE unit_id = @unit_id AND status = 'A'";

            SqlParameter[] parameters = {
                new SqlParameter("@unit_id", unitId),
                new SqlParameter("@code", code),
                new SqlParameter("@name", name),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteUnit(int unitId, int updatedBy)
        {
            string query = @"
                UPDATE Unit
                SET status = 'I',
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE unit_id = @unit_id";

            SqlParameter[] parameters = {
                new SqlParameter("@unit_id", unitId),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable SearchUnits(string keyword)
        {
            string query = @"
                SELECT * FROM Unit 
                WHERE status = 'A' AND (
                    code LIKE @keyword OR
                    name LIKE @keyword
                )
                ORDER BY name";

            SqlParameter[] parameters = { new SqlParameter("@keyword", "%" + keyword + "%") };
            return Connection.ExecuteQuery(query, parameters);
        }

        #endregion

        #region Product Methods

        public DataTable GetProducts()
        {
            string query = @"
                SELECT
                    p.*,
                    c.category_name,
                    b.brand_name,
                    u.code as unit_code
                FROM Product p
                LEFT JOIN Category c ON p.category_id = c.category_id
                LEFT JOIN Brand b ON p.brand_id = b.brand_id
                JOIN Unit u ON p.unit_id = u.unit_id
                WHERE p.status = 'A'
                ORDER BY p.product_name";

            return Connection.ExecuteQuery(query);
        }

        public DataTable GetProductById(int productId)
        {
            string query = @"
                SELECT * FROM Product
                WHERE product_id = @product_id AND status = 'A'";

            SqlParameter[] parameters = { new SqlParameter("@product_id", productId) };
            return Connection.ExecuteQuery(query, parameters);
        }

        public int InsertProduct(string productName, string productCode, string barcode, string productType,
                                 int? categoryId, int? brandId, int unitId,
                                 decimal? purchaseCost, decimal sellingPrice, decimal stockQuantity,
                                 DateTime? expiryDate, DateTime? manufactureDate, string description,
                                 int createdBy)
        {
            string query = @"
                INSERT INTO Product (
                    product_name, product_code, barcode, product_type,
                    category_id, brand_id, unit_id,
                    purchase_cost, selling_price, stock_quantity,
                    expiry_date, manufacture_date, description,
                    status, created_by, created_date
                )
                VALUES (
                    @product_name, @product_code, @barcode, @product_type,
                    @category_id, @brand_id, @unit_id,
                    @purchase_cost, @selling_price, @stock_quantity,
                    @expiry_date, @manufacture_date, @description,
                    'A', @created_by, GETDATE()
                );
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            SqlParameter[] parameters = {
                new SqlParameter("@product_name", productName),
                new SqlParameter("@product_code", productCode),
                new SqlParameter("@barcode", string.IsNullOrWhiteSpace(barcode) ? (object)DBNull.Value : barcode),
                new SqlParameter("@product_type", string.IsNullOrWhiteSpace(productType) ? "Standard" : productType),
                new SqlParameter("@category_id", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value),
                new SqlParameter("@brand_id", brandId.HasValue ? (object)brandId.Value : DBNull.Value),
                new SqlParameter("@unit_id", unitId),
                new SqlParameter("@purchase_cost", purchaseCost.HasValue ? (object)purchaseCost.Value : DBNull.Value),
                new SqlParameter("@selling_price", sellingPrice),
                new SqlParameter("@stock_quantity", stockQuantity),
                new SqlParameter("@expiry_date", expiryDate.HasValue ? (object)expiryDate.Value : DBNull.Value),
                new SqlParameter("@manufacture_date", manufactureDate.HasValue ? (object)manufactureDate.Value : DBNull.Value),
                new SqlParameter("@description", string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description),
                new SqlParameter("@created_by", createdBy)
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
                return Convert.ToInt32(result.Rows[0][0]);

            throw new Exception("Failed to insert Product.");
        }

        public bool UpdateProduct(int productId, string productName, string productCode, string barcode, string productType,
                                  int? categoryId, int? brandId, int unitId,
                                  decimal? purchaseCost, decimal sellingPrice, decimal stockQuantity,
                                  DateTime? expiryDate, DateTime? manufactureDate, string description,
                                  int updatedBy)
        {
            string query = @"
                UPDATE Product
                SET product_name = @product_name,
                    product_code = @product_code,
                    barcode = @barcode,
                    product_type = @product_type,
                    category_id = @category_id,
                    brand_id = @brand_id,
                    unit_id = @unit_id,
                    purchase_cost = @purchase_cost,
                    selling_price = @selling_price,
                    stock_quantity = @stock_quantity,
                    expiry_date = @expiry_date,
                    manufacture_date = @manufacture_date,
                    description = @description,
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE product_id = @product_id AND status = 'A'";

            SqlParameter[] parameters = {
                new SqlParameter("@product_id", productId),
                new SqlParameter("@product_name", productName),
                new SqlParameter("@product_code", productCode),
                new SqlParameter("@barcode", string.IsNullOrWhiteSpace(barcode) ? (object)DBNull.Value : barcode),
                new SqlParameter("@product_type", string.IsNullOrWhiteSpace(productType) ? "Standard" : productType),
                new SqlParameter("@category_id", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value),
                new SqlParameter("@brand_id", brandId.HasValue ? (object)brandId.Value : DBNull.Value),
                new SqlParameter("@unit_id", unitId),
                new SqlParameter("@purchase_cost", purchaseCost.HasValue ? (object)purchaseCost.Value : DBNull.Value),
                new SqlParameter("@selling_price", sellingPrice),
                new SqlParameter("@stock_quantity", stockQuantity),
                new SqlParameter("@expiry_date", expiryDate.HasValue ? (object)expiryDate.Value : DBNull.Value),
                new SqlParameter("@manufacture_date", manufactureDate.HasValue ? (object)manufactureDate.Value : DBNull.Value),
                new SqlParameter("@description", string.IsNullOrWhiteSpace(description) ? (object)DBNull.Value : description),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteProduct(int productId, int updatedBy)
        {
            string query = @"
                UPDATE Product
                SET status = 'I',
                    updated_by = @updated_by,
                    updated_date = GETDATE()
                WHERE product_id = @product_id";

            SqlParameter[] parameters = {
                new SqlParameter("@product_id", productId),
                new SqlParameter("@updated_by", updatedBy)
            };

            return Connection.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable SearchProducts(string keyword)
        {
            string query = @"
                SELECT
                    p.*,
                    c.category_name,
                    b.brand_name,
                    u.code as unit_code
                FROM Product p
                LEFT JOIN Category c ON p.category_id = c.category_id
                LEFT JOIN Brand b ON p.brand_id = b.brand_id
                JOIN Unit u ON p.unit_id = u.unit_id
                WHERE p.status = 'A' AND (
                    p.product_name LIKE @keyword OR
                    p.product_code LIKE @keyword OR
                    p.barcode LIKE @keyword OR
                    c.category_name LIKE @keyword OR
                    b.brand_name LIKE @keyword
                )
                ORDER BY p.product_name";

            SqlParameter[] parameters = { new SqlParameter("@keyword", "%" + keyword + "%") };
            return Connection.ExecuteQuery(query, parameters);
        }

        #endregion

        #region Barcode Print Methods

        public DataTable GetBarcodePrints()
        {
            string query = @"
                SELECT bp.*, p.product_name, p.product_code
                FROM BarcodePrint bp
                JOIN Product p ON bp.product_id = p.product_id
                WHERE bp.status = 'A'";
            return Connection.ExecuteQuery(query);
        }

        public DataTable SearchBarcodePrints(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetBarcodePrints();
            }

            string query = @"
                SELECT bp.*, p.product_name, p.product_code
                FROM BarcodePrint bp
                JOIN Product p ON bp.product_id = p.product_id
                WHERE bp.status = 'A' AND (p.product_name LIKE @keyword OR p.product_code LIKE @keyword)";
            
            SqlParameter[] parameters = {
                new SqlParameter("@keyword", "%" + keyword + "%")
            };

            return Connection.ExecuteQuery(query, parameters);
        }

        public int InsertBarcodePrint(int productId, int quantity, bool includeName, bool includePrice,
                                      bool includeExpiry, bool includeManufacture, bool includePromo, int createdBy)
        {
            string query = @"
                INSERT INTO BarcodePrint (
                    product_id, quantity_printed, include_name, include_price,
                    include_expiry, include_manufacture, include_promo_price,
                    printed_by, status, created_by, created_date
                )
                VALUES (
                    @product_id, @quantity, @include_name, @include_price,
                    @include_expiry, @include_manufacture, @include_promo,
                    @created_by, 'A', @created_by, GETDATE()
                );
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

             SqlParameter[] parameters = {
                new SqlParameter("@product_id", productId),
                new SqlParameter("@quantity", quantity),
                new SqlParameter("@include_name", includeName),
                new SqlParameter("@include_price", includePrice),
                new SqlParameter("@include_expiry", includeExpiry),
                new SqlParameter("@include_manufacture", includeManufacture),
                new SqlParameter("@include_promo", includePromo),
                new SqlParameter("@created_by", createdBy)
            };

            DataTable result = Connection.ExecuteQuery(query, parameters);
            if (result.Rows.Count > 0)
                return Convert.ToInt32(result.Rows[0][0]);

            throw new Exception("Failed to insert Barcode Print job.");
        }

        public void ClearBarcodePrints()
        {
            string query = "DELETE FROM BarcodePrint";
            Connection.ExecuteNonQuery(query);
        }

        #endregion
    }
}
