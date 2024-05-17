import { mapToPosts, Post } from "../posts/post";
import { Department, mapToDepartment } from "./department";

export class DepartmentStructure {
    constructor(
        public department: Department,
        public posts: Post[]
    ) { }
}

export function mapToDepartmentStructure(data: any): DepartmentStructure {
    const department = mapToDepartment(data.department);
    const posts = mapToPosts(data.posts);

    return new DepartmentStructure(department, posts);
}

export function mapToDepartmentStructures(data: any[]): DepartmentStructure[] {
    return data.map(mapToDepartmentStructure);
}