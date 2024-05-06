import axios from "axios";
import { mapToDepartments } from "./department";

export class DepartmentsProvider {
    public static async getDepartments() {
        const any = await axios.get('/departments/getDepartments');
        return mapToDepartments(any.data);
    }
}