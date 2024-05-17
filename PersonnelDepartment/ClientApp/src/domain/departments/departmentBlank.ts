import { Department } from "./department";

export class DepartmentBlank {
    constructor(
        public id: string | null,
        public name: string | null,
        public phoneNumber: string | null
    ) { }

    public static empty(): DepartmentBlank {
        return new DepartmentBlank(null, null, null);
    }

    public static fromDepartment(department: Department): DepartmentBlank {
        return new DepartmentBlank(department.id, department.name, department.phoneNumber);
    }
}