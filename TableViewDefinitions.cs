namespace CourseWork_5sem;

public class TableViewDefinitions
{
    
    public struct TableViewMetadata
    {
        public string DisplaySql;
        public List<string> ColumnNames;
    }
    
    
    public static TableViewMetadata CreateEmployeeMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                e.id, e.last_name, e.first_name, e.middle_name, e.gender, 
                e.birth_date, e.house_number, e.work_experience,
                c.name AS country_name, ci.name AS city_name, s.name AS street_name 
            FROM employees e
            LEFT JOIN countries c ON e.country_id = c.id
            LEFT JOIN cities ci ON e.city_id = ci.id
            LEFT JOIN streets s ON e.street_id = s.id
            ORDER BY e.id",
        ColumnNames = new List<string> { 
            "id", "last_name", "first_name", "middle_name", "gender", "birth_date", 
            "house_number", "work_experience", "country_name", "city_name", "street_name" 
        }
    };
    
    
    public static TableViewMetadata CreateWorkRecordsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                wr.id,
                wr.record_date,
                wr.event_type,
                wr.termination_reason,
                e.last_name || ' ' || e.first_name AS employee_full_name,
                p.name AS position_name,
                s.name AS specialty_name,
                w.name AS workplace_name,
                d.name AS department_name,
                q.name AS qualification_name,
                pr.name AS profession_name
            FROM work_records wr
            LEFT JOIN employees e ON wr.employee_id = e.id
            LEFT JOIN positions p ON wr.position_id = p.id
            LEFT JOIN specialties s ON wr.specialty_id = s.id
            LEFT JOIN workplaces w ON wr.workplace_id = w.id
            LEFT JOIN departments d ON wr.department_id = d.id
            LEFT JOIN qualifications q ON wr.qualification_id = q.id
            LEFT JOIN professions pr ON wr.profession_id = pr.id
            ORDER BY wr.record_date DESC",
        ColumnNames = new List<string> { 
            "id", "record_date", "event_type", "termination_reason", 
            "employee_full_name", "position_name", "specialty_name", 
            "workplace_name", "department_name", "qualification_name", 
            "profession_name" 
        }
    };

    
    public static TableViewMetadata CreatePartsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                p.id, p.name, p.article,
                m.name AS manufacturer_name,
                c.name AS country_name
            FROM parts p
            LEFT JOIN manufacturers m ON p.manufacturer_id = m.id
            LEFT JOIN countries c ON p.country_id = c.id
            ORDER BY p.id",
        ColumnNames = new List<string> { 
            "id", "название", "артикул", "manufacturer_name", "страна" 
        }
    };
    
    
    public static TableViewMetadata CreateSuppliesMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                s.id,
                sp.name AS supplier_name,
                p.name AS part_name,
                s.part_quantity
            FROM supplies s
            LEFT JOIN suppliers sp ON s.supplier_id = sp.id
            LEFT JOIN parts p ON s.part_id = p.id
            ORDER BY s.id",
        ColumnNames = new List<string> { 
            "id", "supplier_name", "part_name", "part_quantity" 
        }
    };

    
    public static TableViewMetadata CreateContractsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                c.id, c.contract_number, c.sign_date, c.discount, c.warranty_period,
                sp.name AS supplier_name,
                e.last_name || ' ' || e.first_name AS employee_full_name
            FROM contracts c
            LEFT JOIN suppliers sp ON c.supplier_id = sp.id
            LEFT JOIN employees e ON c.employee_id = e.id
            ORDER BY c.sign_date DESC",
        ColumnNames = new List<string> { 
            "id", "contract_number", "sign_date", "discount", "warranty_period", 
            "supplier_name", "employee_full_name" 
        }
    };

    
    public static TableViewMetadata CreateDefectsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                d.id,
                p.name AS part_name,
                sp.name AS supplier_name,
                m.name AS manufacturer_name
            FROM defects d
            LEFT JOIN parts p ON d.part_id = p.id
            LEFT JOIN suppliers sp ON d.supplier_id = sp.id
            LEFT JOIN manufacturers m ON d.manufacturer_id = m.id
            ORDER BY d.id",
        ColumnNames = new List<string> { 
            "id", "part_name", "supplier_name", "manufacturer_name" 
        }
    };

    
    public static TableViewMetadata CreateCustomerOrdersMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                co.id, co.order_date, co.order_amount, co.status,
                e.last_name || ' ' || e.first_name AS employee_full_name
            FROM customer_orders co
            LEFT JOIN employees e ON co.employee_id = e.id
            ORDER BY co.order_date DESC",
        ColumnNames = new List<string> { 
            "id", "order_date", "order_amount", "status", "employee_full_name" 
        }
    };

    public static TableViewMetadata CreateOrderPartsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
        SELECT
            op.id,
            -- Заменяем ID заказа на его описательную строку
            'Заказ №' || co.id || ' от ' || TO_CHAR(co.order_date, 'YYYY-MM-DD') AS order_display, 
            p.name AS part_name,
            op.part_quantity
        FROM order_parts op
        LEFT JOIN customer_orders co ON op.order_id = co.id
        LEFT JOIN parts p ON op.part_id = p.id
        ORDER BY op.id",
        ColumnNames = new List<string> { 
            "id", "order_display", "part_name", "part_quantity" 
        }
    };

    
    public static TableViewMetadata CreateCellPartsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                cp.id,
                c.cell_number,
                p.name AS part_name
            FROM cell_parts cp
            LEFT JOIN cells c ON cp.cell_id = c.id
            LEFT JOIN parts p ON cp.part_id = p.id
            ORDER BY cp.id",
        ColumnNames = new List<string> { 
            "id", "cell_number", "part_name" 
        }
    };

    
    public static TableViewMetadata CreateProductRequestsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
            SELECT
                pr.id, pr.creation_date, pr.status, pr.request_amount, pr.operation_amount, pr.operation_date,
                b.last_name || ' ' || b.first_name AS buyer_full_name
            FROM product_requests pr
            LEFT JOIN buyers b ON pr.buyer_id = b.id
            ORDER BY pr.creation_date DESC",
        ColumnNames = new List<string> { 
            "id", "creation_date", "status", "request_amount", "operation_amount", "operation_date", 
            "buyer_full_name" 
        }
    };

    
    public static TableViewMetadata CreateRequestPartsMetadata() => new TableViewMetadata
    {
        DisplaySql = @"
        SELECT
            rp.id,
            -- Заменяем ID заявки на ее описательную строку
            'Заявка №' || pr.id || ' от ' || TO_CHAR(pr.creation_date, 'YYYY-MM-DD') AS product_request_display,
            p.name AS part_name,
            rp.part_quantity
        FROM request_parts rp
        LEFT JOIN product_requests pr ON rp.product_request_id = pr.id
        LEFT JOIN parts p ON rp.part_id = p.id
        ORDER BY rp.id",
        ColumnNames = new List<string> { 
            "id", "product_request_display", "part_name", "part_quantity"    
        }
    };
}