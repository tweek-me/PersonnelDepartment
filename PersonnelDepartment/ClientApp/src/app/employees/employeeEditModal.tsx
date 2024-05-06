import { Box, Dialog, DialogContent, DialogTitle, Grid } from "@mui/material";
import { ClearIcon } from "@mui/x-date-pickers";
import { useEffect, useState } from "react";
import { Department } from "../../domain/departments/department";
import { EmployeeBlank } from "../../domain/employees/employeeBlank";
import { EmployeeProvider } from "../../domain/employees/employeeProvider";
import { Post } from "../../domain/posts/post";
import { PostsProvider } from "../../domain/posts/postsProvider";
import { DepartmentsProvider } from "../../domain/departments/departmentsProvider";

interface IProps {
    employeeId: string | null
    setIsOpen: (isOpen: boolean) => void;
}

export function EmployeeEditModal(props: IProps) {

    const [employeeBlank, setEmployeeBlank] = useState<EmployeeBlank>(EmployeeBlank.empty());
    const [departments, setDepartments] = useState<Department[]>([]);
    const [posts, setPosts] = useState<Post[]>([]);

    //TASK ILYA сделать blockUi
    useEffect(() => {
        async function init() {
            if (props.employeeId == null) return;

            const employee = await EmployeeProvider.get(props.employeeId);
            const departments = await DepartmentsProvider.getDepartments();

            setEmployeeBlank(employee);
            setDepartments(departments);
        }

        init();
    }, [props.employeeId])

    useEffect(() => {
        async function init() {
            if (employeeBlank.departmentId == null) return;

            const posts = await PostsProvider.getPosts(employeeBlank.departmentId);
            setPosts(posts);
        }

        init();
    }, [employeeBlank.departmentId])

    return (
        <Dialog open onClose={() => props.setIsOpen(false)}>
            <DialogTitle>
                <Box display={'flex'} alignItems={'center'} justifyContent={'space-between'}>
                    Редактирование работника
                    <ClearIcon />
                </Box>
            </DialogTitle>
            <DialogContent>
                <Grid container spacing={2}>

                </Grid>
            </DialogContent>
        </Dialog>
    )
}