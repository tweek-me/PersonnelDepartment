import axios from "axios";
import { mapToPost, mapToPosts, Post } from "./post";

export class PostsProvider {
    public static async get(postId: string): Promise<Post | null> {
        const any = await axios.get('/posts/get', { params: { postId } })
        //TASK ILYA с сервера может вернуться null
        return mapToPost(any);
    }

    public static async getPosts(departmentId: string): Promise<Post[]> {
        const any = await axios.get('/posts/getPosts', { params: { departmentId } })
        return mapToPosts(any.data);
    }
}