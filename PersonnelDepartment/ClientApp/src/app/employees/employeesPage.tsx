import EditIcon from '@mui/icons-material/Edit';
import PersonRemoveIcon from '@mui/icons-material/PersonRemove';
import { Box, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import { CPagination } from '../../components/pagination/cPagination';
import { Employee } from "../../domain/employees/employee";
import { EmployeeProvider } from "../../domain/employees/employeeProvider";
import { EmployeeEditModal } from './employeeEditModal';

interface Pagination {
    page: number,
    pageSize: number,
    totalRows: number
}

export function EmployeesPage() {

    const [employees, setEmployees] = useState<Employee[]>([]);
    const [pagination, setPagination] = useState<Pagination>({
        page: 1,
        pageSize: 15,
        totalRows: 0
    })

    const [isEditModalOpen, setIsEditModalOpen] = useState<boolean>(false);

    useEffect(() => {
        async function init() {
            const employeesPage = await EmployeeProvider.getPage(pagination.page, pagination.pageSize);
            setPagination(pagination => ({ ...pagination, totalRows: employeesPage.totalRows }));
            setEmployees(employeesPage.values);
        }

        init();
    }, [])

    function buildEmployeeFIO(employee: Employee) {
        return `${employee.surname} ${employee.name} ${employee.partronymic}`;
    }

    return (
        <Box>
            <CPagination
                pageSize={pagination.pageSize}
                onChangePage={page => setPagination(pagination => ({ ...pagination, page }))}
                totalRows={pagination.totalRows}
            />
            <TableContainer component={Paper} sx={{ borderRadius: 3 }}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell align='center'>ФИО</TableCell>
                            <TableCell align='center'>Номер телефона</TableCell>
                            <TableCell align='center'>ИНН</TableCell>
                            <TableCell align='center'>СНИЛС</TableCell>
                            <TableCell align='center'>Уволен</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            employees.length !== 0
                                ? employees.map(employee => (
                                    <TableRow hover>
                                        <TableCell>{buildEmployeeFIO(employee)}</TableCell>
                                        <TableCell>{employee.phoneNumber}</TableCell>
                                        <TableCell>{employee.inn}</TableCell>
                                        <TableCell>{employee.snils}</TableCell>
                                        <TableCell align='center'>{employee.isDismissed ? "Да" : "Нет"}</TableCell>
                                        <TableCell>
                                            <Box justifyContent={'flex-end'} display={'flex'} gap={2}>
                                                <IconButton onClick={() => setIsEditModalOpen(true)}>
                                                    <EditIcon />
                                                </IconButton>
                                                <IconButton>
                                                    <PersonRemoveIcon />
                                                </IconButton>
                                            </Box>
                                        </TableCell>
                                    </TableRow>
                                ))

                                : <TableRow>
                                    <TableCell align="center" colSpan={5}>Работники отсутствуют</TableCell>
                                </TableRow>
                        }
                    </TableBody>
                </Table>
            </TableContainer>

            {
                isEditModalOpen &&
                <EmployeeEditModal setIsOpen={setIsEditModalOpen} />
            }
        </Box>
    )
}