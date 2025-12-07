public enum ColumnType { Text, Date, Numeric, Boolean, Enum, Unknown }

public struct ReferenceItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public override string ToString() => Name;
}

public class ColumnMetadata
{
    public string DisplayName { get; set; }  // Имя в DataRow (напр., 'country_name')
    public string SourceColumn { get; set; } // Имя в таблице БД (напр., 'country_id')
    public bool IsForeignKey { get; set; }   
    public string ReferenceTable { get; set; } // Если FK (напр., 'countries')
    public ColumnType Type { get; set; }     // Тип для не-FK полей 
    public string EnumTypeName { get; set; } // Если Type == Enum 
}

// --- НОВЫЙ КЛАСС ДЛЯ МЕТАДАННЫХ РЕДАКТИРОВАНИЯ ---
public static class EditMetadata
{
    public static readonly Dictionary<string, List<ColumnMetadata>> TableEditDefinitions = 
        new Dictionary<string, List<ColumnMetadata>>
    {
        // -----------------------------------------------------
        // 1. ТАБЛИЦА: countries
        // -----------------------------------------------------
        ["countries"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        
        // -----------------------------------------------------
        // 2. ТАБЛИЦА: cities
        // -----------------------------------------------------
        ["cities"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        
        // -----------------------------------------------------
        // 3. ТАБЛИЦА: streets
        // -----------------------------------------------------
        ["streets"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },

        // -----------------------------------------------------
        // 4. ТАБЛИЦА: employees
        // -----------------------------------------------------
        ["employees"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
            new ColumnMetadata { DisplayName = "last_name", SourceColumn = "last_name", Type = ColumnType.Text },
            new ColumnMetadata { DisplayName = "first_name", SourceColumn = "first_name", Type = ColumnType.Text },
            new ColumnMetadata { DisplayName = "middle_name", SourceColumn = "middle_name", Type = ColumnType.Text },
            
            // ENUM: gender_enum
            new ColumnMetadata { DisplayName = "gender", SourceColumn = "gender", Type = ColumnType.Enum, EnumTypeName = "gender_enum" }, 
            
            new ColumnMetadata { DisplayName = "birth_date", SourceColumn = "birth_date", Type = ColumnType.Date },
            new ColumnMetadata { DisplayName = "house_number", SourceColumn = "house_number", Type = ColumnType.Text }, 
            new ColumnMetadata { DisplayName = "work_experience", SourceColumn = "work_experience", Type = ColumnType.Numeric },
            
            // FK
            new ColumnMetadata { DisplayName = "country_name", SourceColumn = "country_id", IsForeignKey = true, ReferenceTable = "countries" },
            new ColumnMetadata { DisplayName = "city_name", SourceColumn = "city_id", IsForeignKey = true, ReferenceTable = "cities" },
            new ColumnMetadata { DisplayName = "street_name", SourceColumn = "street_id", IsForeignKey = true, ReferenceTable = "streets" },
        },
        
        // -----------------------------------------------------
        // 5-10. Справочники
        // -----------------------------------------------------
        ["positions"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        ["specialties"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        ["workplaces"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        ["departments"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        ["qualifications"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        ["professions"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },
        
        // -----------------------------------------------------
        // 11. ТАБЛИЦА: work_records
        // -----------------------------------------------------
        ["work_records"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            new ColumnMetadata { DisplayName = "record_date", SourceColumn = "record_date", Type = ColumnType.Date },
            
            // ENUM: event_type
            new ColumnMetadata { DisplayName = "event_type", SourceColumn = "event_type", Type = ColumnType.Enum, EnumTypeName = "event_type" }, 
            
            new ColumnMetadata { DisplayName = "termination_reason", SourceColumn = "termination_reason", Type = ColumnType.Text },
            
            // FK
            new ColumnMetadata { DisplayName = "employee_full_name", SourceColumn = "employee_id", IsForeignKey = true, ReferenceTable = "employees" },
            new ColumnMetadata { DisplayName = "position_name", SourceColumn = "position_id", IsForeignKey = true, ReferenceTable = "positions" },
            new ColumnMetadata { DisplayName = "specialty_name", SourceColumn = "specialty_id", IsForeignKey = true, ReferenceTable = "specialties" },
            new ColumnMetadata { DisplayName = "workplace_name", SourceColumn = "workplace_id", IsForeignKey = true, ReferenceTable = "workplaces" },
            new ColumnMetadata { DisplayName = "department_name", SourceColumn = "department_id", IsForeignKey = true, ReferenceTable = "departments" },
            new ColumnMetadata { DisplayName = "qualification_name", SourceColumn = "qualification_id", IsForeignKey = true, ReferenceTable = "qualifications" },
            new ColumnMetadata { DisplayName = "profession_name", SourceColumn = "profession_id", IsForeignKey = true, ReferenceTable = "professions" },
        },

        // -----------------------------------------------------
        // 12. ТАБЛИЦА: manufacturers
        // -----------------------------------------------------
        ["manufacturers"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
        },

        // -----------------------------------------------------
        // 13. ТАБЛИЦА: parts
        // -----------------------------------------------------
        ["parts"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
            new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
            new ColumnMetadata { DisplayName = "article", SourceColumn = "article", Type = ColumnType.Text },
            
            // FK
            new ColumnMetadata { DisplayName = "manufacturer_name", SourceColumn = "manufacturer_id", IsForeignKey = true, ReferenceTable = "manufacturers" },
            new ColumnMetadata { DisplayName = "country_name", SourceColumn = "country_id", IsForeignKey = true, ReferenceTable = "countries" },
        },
        
        // -----------------------------------------------------
        // 14. ТАБЛИЦА: suppliers
        // -----------------------------------------------------
        ["suppliers"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
            new ColumnMetadata { DisplayName = "name", SourceColumn = "name", Type = ColumnType.Text },
            // ENUM: supplier_category_type
            new ColumnMetadata { DisplayName = "category", SourceColumn = "category", Type = ColumnType.Enum, EnumTypeName = "supplier_category_type" },
            new ColumnMetadata { DisplayName = "phone_number", SourceColumn = "phone_number", Type = ColumnType.Text },
        },

        // -----------------------------------------------------
        // 15. ТАБЛИЦА: supplies
        // -----------------------------------------------------
        ["supplies"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
            
            // FK
            new ColumnMetadata { DisplayName = "supplier_name", SourceColumn = "supplier_id", IsForeignKey = true, ReferenceTable = "suppliers" },
            new ColumnMetadata { DisplayName = "part_name", SourceColumn = "part_id", IsForeignKey = true, ReferenceTable = "parts" },
            
            new ColumnMetadata { DisplayName = "part_quantity", SourceColumn = "part_quantity", Type = ColumnType.Numeric },
        },

        // -----------------------------------------------------
        // 16. ТАБЛИЦА: contracts
        // -----------------------------------------------------
        ["contracts"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            new ColumnMetadata { DisplayName = "contract_number", SourceColumn = "contract_number", Type = ColumnType.Text },
            new ColumnMetadata { DisplayName = "sign_date", SourceColumn = "sign_date", Type = ColumnType.Date },
            new ColumnMetadata { DisplayName = "discount", SourceColumn = "discount", Type = ColumnType.Numeric },
            new ColumnMetadata { DisplayName = "warranty_period", SourceColumn = "warranty_period", Type = ColumnType.Numeric }, 
            
            // FK
            new ColumnMetadata { DisplayName = "supplier_name", SourceColumn = "supplier_id", IsForeignKey = true, ReferenceTable = "suppliers" },
            new ColumnMetadata { DisplayName = "employee_full_name", SourceColumn = "employee_id", IsForeignKey = true, ReferenceTable = "employees" },
        },

        // -----------------------------------------------------
        // 17. ТАБЛИЦА: defects
        // -----------------------------------------------------
        ["defects"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            
            // FK
            new ColumnMetadata { DisplayName = "part_name", SourceColumn = "part_id", IsForeignKey = true, ReferenceTable = "parts" },
            new ColumnMetadata { DisplayName = "supplier_name", SourceColumn = "supplier_id", IsForeignKey = true, ReferenceTable = "suppliers" },
            new ColumnMetadata { DisplayName = "manufacturer_name", SourceColumn = "manufacturer_id", IsForeignKey = true, ReferenceTable = "manufacturers" },
        },

        // -----------------------------------------------------
        // 18. ТАБЛИЦА: customer_orders
        // -----------------------------------------------------
        ["customer_orders"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
            new ColumnMetadata { DisplayName = "order_date", SourceColumn = "order_date", Type = ColumnType.Date },
            new ColumnMetadata { DisplayName = "order_amount", SourceColumn = "order_amount", Type = ColumnType.Numeric },
            
            // ENUM: order_status_type
            new ColumnMetadata { DisplayName = "status", SourceColumn = "status", Type = ColumnType.Enum, EnumTypeName = "order_status_type" }, 
            
            // FK
            new ColumnMetadata { DisplayName = "employee_full_name", SourceColumn = "employee_id", IsForeignKey = true, ReferenceTable = "employees" },
        },

        // -----------------------------------------------------
        // 19. ТАБЛИЦА: order_parts
        // -----------------------------------------------------
        ["order_parts"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            
            // FK
            new ColumnMetadata { DisplayName = "order_id", SourceColumn = "order_id", IsForeignKey = true, ReferenceTable = "customer_orders" },
            new ColumnMetadata { DisplayName = "part_name", SourceColumn = "part_id", IsForeignKey = true, ReferenceTable = "parts" },
            
            new ColumnMetadata { DisplayName = "part_quantity", SourceColumn = "part_quantity", Type = ColumnType.Numeric },
        },

        // -----------------------------------------------------
        // 20. ТАБЛИЦА: cells
        // -----------------------------------------------------
        ["cells"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "cell_number", SourceColumn = "cell_number", Type = ColumnType.Text },
             new ColumnMetadata { DisplayName = "capacity", SourceColumn = "capacity", Type = ColumnType.Numeric },
        },

        // -----------------------------------------------------
        // 21. ТАБЛИЦА: cell_parts
        // -----------------------------------------------------
        ["cell_parts"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            
            // FK
            new ColumnMetadata { DisplayName = "cell_number", SourceColumn = "cell_id", IsForeignKey = true, ReferenceTable = "cells" },
            new ColumnMetadata { DisplayName = "part_name", SourceColumn = "part_id", IsForeignKey = true, ReferenceTable = "parts" },
        },
        
        // -----------------------------------------------------
        // 22. ТАБЛИЦА: buyers
        // -----------------------------------------------------
        ["buyers"] = new List<ColumnMetadata>
        {
             new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric }, 
             new ColumnMetadata { DisplayName = "last_name", SourceColumn = "last_name", Type = ColumnType.Text },
             new ColumnMetadata { DisplayName = "first_name", SourceColumn = "first_name", Type = ColumnType.Text },
             new ColumnMetadata { DisplayName = "middle_name", SourceColumn = "middle_name", Type = ColumnType.Text },
        },

        // -----------------------------------------------------
        // 23. ТАБЛИЦА: product_requests
        // -----------------------------------------------------
        ["product_requests"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            new ColumnMetadata { DisplayName = "creation_date", SourceColumn = "creation_date", Type = ColumnType.Date },
            
            // ENUM: request_status_type
            new ColumnMetadata { DisplayName = "status", SourceColumn = "status", Type = ColumnType.Enum, EnumTypeName = "request_status_type" }, 
            
            // FK
            new ColumnMetadata { DisplayName = "buyer_full_name", SourceColumn = "buyer_id", IsForeignKey = true, ReferenceTable = "buyers" },
            
            new ColumnMetadata { DisplayName = "request_amount", SourceColumn = "request_amount", Type = ColumnType.Numeric },
            new ColumnMetadata { DisplayName = "operation_amount", SourceColumn = "operation_amount", Type = ColumnType.Numeric },
            new ColumnMetadata { DisplayName = "operation_date", SourceColumn = "operation_date", Type = ColumnType.Date },
        },

        // -----------------------------------------------------
        // 24. ТАБЛИЦА: request_parts
        // -----------------------------------------------------
        ["request_parts"] = new List<ColumnMetadata>
        {
            new ColumnMetadata { SourceColumn = "id", Type = ColumnType.Numeric },
            
            // FK
            new ColumnMetadata { DisplayName = "product_request_id", SourceColumn = "product_request_id", IsForeignKey = true, ReferenceTable = "product_requests" },
            new ColumnMetadata { DisplayName = "part_name", SourceColumn = "part_id", IsForeignKey = true, ReferenceTable = "parts" },
            
            new ColumnMetadata { DisplayName = "part_quantity", SourceColumn = "part_quantity", Type = ColumnType.Numeric },
        },
    };
}