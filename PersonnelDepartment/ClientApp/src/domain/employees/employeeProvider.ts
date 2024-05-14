import axios from "axios";
import { Page } from "../../tools/page";
import { Result } from "../../tools/result/result";
import { Employee, mapToEmployee, mapToEmployees } from "./employee";
import { EmployeeBlank } from "./employeeBlank";

export class EmployeeProvider {
    public static async save(employeeBlank: EmployeeBlank): Promise<Result> {
        const result = await axios.post('/employees/save', { employeeBlank });
        return result.data.isSuccess ? Result.success() : Result.failed(result.data.errors);
    }

    public static async getPage(page: number, pageSize: number): Promise<Page<Employee>> {
        const any = await axios.get('/employees/getPage', { params: { page, pageSize } });
        return new Page(mapToEmployees(any.data.values), any.data.totalRows);
    }

    public static async get(employeeId: string): Promise<Employee> {
        const any = await axios.get('/employees/get', { params: { employeeId } });
        return mapToEmployee(any.data);
    }

    public static async remove(employeeId: string): Promise<Result> {
        const result = await axios.post('/employees/remove', { employeeId });
        return result.data.isSuccess ? Result.success() : Result.failed(result.data.errors);
    }
}