export class Department {
    constructor(
        public id: string,
        public name: string,
        public phoneNumber: string
    ) { }
}

export function mapToDepartment(data: any): Department {
    return new Department(data.id, data.name, data.phoneNumber);
}

export function mapToDepartments(data: any[]): Department[] {
    return data.map(mapToDepartment);
}