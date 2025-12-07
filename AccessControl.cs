namespace CourseWork_5sem;

public static class AccessControl
{
    // --- 1. УДАЛЕНО: Словарь _rolePermissions и метод GetAccessibleTables ---
    // Они больше не нужны, так как права доступа теперь определяются базой данных PostgreSQL.

    // --- 2. ОСТАВЛЕНО: Словарь для преобразования имен таблиц (Display Name Map) ---
    // Это ключевой элемент для перевода "buyers" в "Покупатели" для меню.
    public static readonly Dictionary<string, string> TableDisplayNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // Справочники
        { "countries", "Страны" }, { "cities", "Города" }, { "streets", "Улицы" },
        
        // Клиентский отдел
        { "buyers", "Покупатели" }, { "product_requests", "Запросы товаров" }, { "request_parts", "Детали запросов" },
        
        // Бухгалтерия
        { "employees", "Сотрудники" }, { "positions", "Должности" }, { "specialties", "Специальности" },
        { "workplaces", "Рабочие места" }, { "departments", "Отделы" }, { "qualifications", "Квалификации" },
        { "professions", "Профессии" }, { "work_records", "Учет работ" },

        // Закупки
        { "suppliers", "Поставщики" }, { "parts", "Запчасти" }, { "manufacturers", "Производители" },
        { "supplies", "Поставки" }, { "contracts", "Контракты" }, { "defects", "Брак" },
        { "order_parts", "Заказ запчастей" }, { "customer_orders", "Заказы клиентов" },
        { "cell_parts", "Ячейки с деталями" }, { "cells", "Ячейки склада" }
    };
    
    // --- 3. НОВЫЙ ЭЛЕМЕНТ: Множество для классификации таблиц (Reference List) ---
    // Это список, который MainForm использует, чтобы разделить таблицы, 
    // полученные из БД, на группу "Справочники".
    public static readonly HashSet<string> ReferenceTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "countries",
        "cities",
        "streets"
    };
    
    public struct TableViewMetadata
    {
        // Полный SQL-запрос для отображения (замена FK)
        public string DisplaySql; 
        
        // Ожидаемый список результирующих имен колонок (для поиска и DataGrid)
        public List<string> ColumnNames;
    }

    // --- 5. Словарь, содержащий метаданные для всех кастомных таблиц ---
    public static readonly Dictionary<string, TableViewDefinitions.TableViewMetadata> CustomTableViews = 
        new Dictionary<string, TableViewDefinitions.TableViewMetadata>(StringComparer.OrdinalIgnoreCase)
        {
            // Теперь вызываем методы из отдельного класса:
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