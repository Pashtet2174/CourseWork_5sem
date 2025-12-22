namespace CourseWork_5sem;

public static class AccessControl
{
    public static readonly Dictionary<string, string> TableDisplayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // Справочники
        { "countries", "Страны" }, { "cities", "Города" }, { "streets", "Улицы" },
        
        // Клиентский отдел
        { "buyers", "Покупатели" }, { "product_requests", "Заявки" }, { "request_parts", "Запчасти в заявках" },
        
        // Бухгалтерия
        { "employees", "Сотрудники" }, { "positions", "Должности" }, { "specialties", "Специальности" },
        { "workplaces", "Место работы" }, { "departments", "Структурное подразделение" }, { "qualifications", "Квалификации" },
        { "professions", "Профессии" }, { "work_records", "Запись в трудовой книжке" },

        // Закупки
        { "suppliers", "Поставщики" }, { "parts", "Запчасти" }, { "manufacturers", "Производители" },
        { "supplies", "Поставки" }, { "contracts", "Контракты" }, { "defects", "Брак" },
        { "order_parts", "Запчасти  в заказах" }, { "customer_orders", "Заказы" },
        { "cell_parts", "Запчасти в ячейках" }, { "cells", "Ячейки" }
    };
    
    public static readonly HashSet<string> ReferenceTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "countries",
        "cities",
        "streets"
    };
    
    public struct TableViewMetadata
    {
        public string DisplaySql; 
        public List<string> ColumnNames;
    }

    public static readonly Dictionary<string, TableViewDefinitions.TableViewMetadata> CustomTableViews = 
        new Dictionary<string, TableViewDefinitions.TableViewMetadata>(StringComparer.OrdinalIgnoreCase)
        {
            { "employees", TableViewDefinitions.CreateEmployeeMetadata() },
            { "work_records", TableViewDefinitions.CreateWorkRecordsMetadata() },
            { "parts", TableViewDefinitions.CreatePartsMetadata() },
            { "supplies",  TableViewDefinitions.CreateSuppliesMetadata() },
            { "contracts",  TableViewDefinitions.CreateContractsMetadata() },
            { "defects", TableViewDefinitions.CreateDefectsMetadata() },
            { "customer_orders", TableViewDefinitions.CreateCustomerOrdersMetadata() },
            { "order_parts", TableViewDefinitions.CreateOrderPartsMetadata() },
            { "cell_parts", TableViewDefinitions.CreateCellPartsMetadata() },
            { "product_requests", TableViewDefinitions.CreateProductRequestsMetadata() },
            { "request_parts", TableViewDefinitions.CreateRequestPartsMetadata() }
        };
}