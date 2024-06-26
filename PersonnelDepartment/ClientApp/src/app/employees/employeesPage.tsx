import EditIcon from '@mui/icons-material/Edit';
import PersonRemoveIcon from '@mui/icons-material/PersonRemove';
import { Box, Button, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import { ConfirmModal } from '../../components/modal/confirmModal';
import { Page } from '../../components/page/page';
import { CPagination } from '../../components/pagination/cPagination';
import { Employee } from "../../domain/employees/employee";
import { EmployeeProvider } from "../../domain/employees/employeeProvider";
import { useNotifications } from '../../hooks/useNotifications';
import { EmployeeEditModal } from './employeeEditModal';

interface Pagination {
    page: number,
    pageSize: number,
    totalRows: number
}

//TASK добавить разделители таблице
export function EmployeesPage() {

    const { addErrorNotification, addSuccessNotification } = useNotifications();

    const [employees, setEmployees] = useState<Employee[]>([]);
    const [pagination, setPagination] = useState<Pagination>({
        page: 1,
        pageSize: 10,
        totalRows: 0
    })

    const [isEditModalOpen, setIsEditModalOpen] = useState<boolean>(false);
    const [isRemoveModalOpen, setIsRemoveModalOpen] = useState<boolean>(false);

    const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null);

    useEffect(() => {
        loadEmployeesPage();
    }, [pagination.page])

    async function loadEmployeesPage() {
        const employeesPage = await EmployeeProvider.getPage(pagination.page, pagination.pageSize);
        setPagination(pagination => ({ ...pagination, totalRows: employeesPage.totalRows }));
        setEmployees(employeesPage.values);
    }

    function buildEmployeeFIO(employee: Employee) {
        return `${employee.surname} ${employee.name} ${employee.partronymic}`;
    }

    function onAddEmployeeClick() {
        setSelectedEmployee(null);
        setIsEditModalOpen(true);
    }

    function onEditEmployeeClick(employee: Employee) {
        setSelectedEmployee(employee);
        setIsEditModalOpen(true)
    }

    function onSaveEmployee() {
        setIsEditModalOpen(false);
        loadEmployeesPage();
    }

    function onRemoveEmployeeClick(employee: Employee) {
        setSelectedEmployee(employee);
        setIsRemoveModalOpen(true);
    }

    async function removeEmployee() {
        if (selectedEmployee == null) return;

        const result = await EmployeeProvider.remove(selectedEmployee.id);
        if (!result.isSuccess) {
            addErrorNotification(result.errors[0].errorMessage);
            return;
        }

        addSuccessNotification('Успешно');
        setIsRemoveModalOpen(false);
        loadEmployeesPage();
    }

    return (
        <Page>
            <Box marginTop={7} marginBottom={2}>
                <Button
                    variant='contained'
                    color='info'
                    onClick={onAddEmployeeClick}
                >
                    Добавить работника
                </Button>
            </Box>
            <TableContainer component={Paper} sx={{ borderRadius: 3 }}>
                <Table>
                    <TableHead sx={{ backgroundColor: '#99CCFF' }}>
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
                                    <TableRow key={employee.id} hover>
                                        <TableCell align='center'>{buildEmployeeFIO(employee)}</TableCell>
                                        <TableCell align='center'>{employee.phoneNumber}</TableCell>
                                        <TableCell align='center'>{employee.inn}</TableCell>
                                        <TableCell align='center'>{employee.snils}</TableCell>
                                        <TableCell align='center'>{employee.isDismissed ? "Да" : "Нет"}</TableCell>
                                        <TableCell>
                                            <Box justifyContent={'flex-end'} display={'flex'} gap={2}>
                                                <IconButton onClick={() => onEditEmployeeClick(employee)}>
                                                    <EditIcon />
                                                </IconButton>
                                                <IconButton onClick={() => onRemoveEmployeeClick(employee)}>
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
            <Box display={'flex'} justifyContent={'center'} padding={2}>
                <CPagination
                    pageSize={pagination.pageSize}
                    onChangePage={page => setPagination(pagination => ({ ...pagination, page }))}
                    totalRows={pagination.totalRows}
                />
            </Box>

            {
                isEditModalOpen &&
                <EmployeeEditModal
                    employeeId={selectedEmployee?.id ?? null}
                    onSave={onSaveEmployee}
                    onClose={() => setIsEditModalOpen(false)}
                />
            }
            {
                isRemoveModalOpen &&
                <ConfirmModal
                    title='Вы уверены, что хотите удалить сотрудника?'
                    onYesClick={removeEmployee}
                    onClose={() => setIsRemoveModalOpen(false)}
                />
            }
        </Page >
    )
}