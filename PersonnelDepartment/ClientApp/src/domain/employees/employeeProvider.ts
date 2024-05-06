import axios from "axios";
import { Page } from "../../tools/page";
import { Employee, mapToEmployee, mapToEmployees } from "./employee";

export class EmployeeProvider {
    public static async getPage(page: number, pageSize: number): Promise<Page<Employee>> {
        const any = await axios.get('/employees/getPage', { params: { page, pageSize } });
        return new Page(mapToEmployees(any.data.values), any.data.totalRows);
    }

    public static async get(employeeId: string): Promise<Employee> {
        const any = await axios.get('/employees/get', { params: { employeeId } })
        return mapToEmployee(any);
    }
}