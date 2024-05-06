export class Employee {
    constructor(
        public id: string,
        public departmentId: string,
        public postId: string,
        public name: string,
        public surname: string,
        public partronymic: string,
        public phoneNumber: string,
        public email: string,
        public inn: string,
        public snils: string,
        public passportSeries: number,
        public passportNumber: number,
        public birthDay: Date,
        public isDismissed: boolean
    ) { }
}

export function mapToEmployee(data: any): Employee {
    return new Employee(
        data.id, data.departmentId, data.postId, data.name, data.surname, data.partronymic,
        data.phoneNumber, data.email, data.inn, data.snils, data.passportSeries,
        data.passportNumber, new Date(data.birthDay), data.isDismissed
    );
}

export function mapToEmployees(data: any[]): Employee[] {
    return data.map(mapToEmployee);
}