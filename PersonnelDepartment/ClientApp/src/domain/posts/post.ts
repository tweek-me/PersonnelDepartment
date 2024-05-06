export class Post {
    constructor(
        public id: string,
        public departmentId: string,
        public name: string,
        public salary: number
    ) { }
}

export function mapToPost(data: any): Post {
    return new Post(data.id, data.departmentId, data.name, data.salary);
}

export function mapToPosts(data: any[]): Post[] {
    return data.map(mapToPost);
}