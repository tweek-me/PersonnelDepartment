import axios from "axios";
import { Result } from "../../tools/result/result";
import { mapToPost, mapToPosts, Post } from "./post";
import { PostBlank } from "./postBlank";

export class PostsProvider {
    public static async savePost(postBlank: PostBlank): Promise<Result> {
        const any = await axios.post('/posts/savePost', { postBlank });
        return any.data.isSuccess ? Result.success() : Result.failed(any.data.errors);
    }

    public static async getPost(postId: string): Promise<Post> {
        const any = await axios.get('/posts/get', { params: { postId } })
        return mapToPost(any);
    }

    public static async getPosts(departmentId: string): Promise<Post[]> {
        const any = await axios.get('/posts/getPosts', { params: { departmentId } })
        return mapToPosts(any.data);
    }

    public static async removePost(postId: string): Promise<Result> {
        const any = await axios.post('/posts/removePost', { postId });
        return any.data.isSuccess ? Result.success() : Result.failed(any.data.errors);
    }
}