import { Post } from "./post";

export class PostBlank {
    constructor(
        public id: string | null,
        public departmentId: string | null,
        public name: string | null,
        public salary: number | null
    ) { }

    public static empty(departmentId: string): PostBlank {
        return new PostBlank(null, departmentId, null, null);
    }

    public static fromPost(post: Post): PostBlank {
        return new PostBlank(post.id, post.departmentId, post.name, post.salary);
    }
}