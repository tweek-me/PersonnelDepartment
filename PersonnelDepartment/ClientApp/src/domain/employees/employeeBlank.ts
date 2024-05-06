import { Employee } from "./employee";

export class EmployeeBlank {
    constructor(
        public id: string | null,
        public departmentId: string | null,
        public postId: string | null,
        public name: string | null,
        public surname: string | null,
        public partronymic: string | null,
        public phoneNumber: string | null,
        public email: string | null,
        public inn: string | null,
        public snils: string | null,
        public passportSeries: number | null,
        public passportNumber: number | null,
        public birthDay: Date | null,
        public isDismissed: boolean
    ) { }

    public static empty(): EmployeeBlank {
        return new EmployeeBlank(null, null, null, null, null, null, null, null, null, null, null, null, null, false);
    }

    public static fromEmployee(employee: Employee): EmployeeBlank {
        return new EmployeeBlank(
            employee.id, employee.departmentId, employee.postId, employee.name, employee.surname,
            employee.partronymic, employee.phoneNumber, employee.email, employee.inn, employee.snils,
            employee.passportSeries, employee.passportNumber, employee.birthDay, employee.isDismissed
        )
    }
}