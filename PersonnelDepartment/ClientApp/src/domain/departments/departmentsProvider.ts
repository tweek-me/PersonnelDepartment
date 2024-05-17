import axios from "axios";
import { Page } from "../../tools/page";
import { Result } from "../../tools/result/result";
import { Department, mapToDepartment, mapToDepartments } from "./department";
import { DepartmentBlank } from "./departmentBlank";
import { DepartmentStructure, mapToDepartmentStructures } from "./departmentStructure";

export class DepartmentsProvider {
    public static async saveDepartment(departmentBlank: DepartmentBlank): Promise<Result> {
        const any = await axios.post('/departments/saveDepartment', { departmentBlank });
        return any.data.isSuccess ? Result.success() : Result.failed(any.data.errors);
    }

    public static async getDepartment(departmentId: string): Promise<Department> {
        const any = await axios.get('/departments/getDepartments', { params: { departmentId } })
        return mapToDepartment(any.data);
    }


    public static async getDepartments(): Promise<Department[]> {
        const any = await axios.get('/departments/getDepartments');
        return mapToDepartments(any.data);
    }

    public static async getDepartmentStructuresPage(page: number, pageSize: number): Promise<Page<DepartmentStructure>> {
        const any = await axios.get('/departments/getDepartmentsPage', { params: { page, pageSize } });
        return new Page(mapToDepartmentStructures(any.data.values), any.data.totalRows);
    }

    public static async removeDepartment(departmentId: string): Promise<Result> {
        const any = await axios.post('/departments/removeDepartment', { departmentId });
        return any.data.isSuccess ? Result.success() : Result.failed(any.data.errors);
    }
}