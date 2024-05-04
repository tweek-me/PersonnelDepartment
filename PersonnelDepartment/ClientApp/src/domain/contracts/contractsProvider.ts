import axios from "axios";

export class ContractsProvider {
    public static async get() {
        const page = 1;
        const pageSize = 10;
        const v = await axios.get('/contracts/getPage', { params: { page, pageSize } })
    }
}