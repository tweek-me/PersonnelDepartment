import { Box, Button, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { useEffect, useState } from "react";
import { ConfirmModal } from "../../components/modal/confirmModal";
import { Page } from "../../components/page/page";
import { CPagination } from "../../components/pagination/cPagination";
import { Department } from "../../domain/departments/department";
import { DepartmentStructure } from "../../domain/departments/departmentStructure";
import { DepartmentsProvider } from "../../domain/departments/departmentsProvider";
import { useNotifications } from "../../hooks/useNotifications";
import { DepartmentEditModal } from "./departmentEditModal";
import { DepartmentTableRow } from './departmentTableRow';

interface Pagination {
    page: number;
    pageSize: number;
    totalRows: number;
}

export function DepartmentPage() {

    const { addErrorNotification, addSuccessNotification } = useNotifications();

    const [departmentStructures, setDepartmentStructures] = useState<DepartmentStructure[]>([]);
    const [pagination, setPagination] = useState<Pagination>({
        page: 1,
        pageSize: 10,
        totalRows: 0
    })

    const [isEditDepartmentModalOpen, setIsEditDepartmentModalOpen] = useState<boolean>(false);
    const [isRemoveDepartmentModalOpen, setIsRemoveDepartmentModalOpen] = useState<boolean>(false);

    const [selectedDepartment, setSelectedDepartment] = useState<Department | null>(null);

    useEffect(() => {
        loadDepartmentStructuresPage();
    }, [pagination.page])

    async function loadDepartmentStructuresPage() {
        const departmentStructures = await DepartmentsProvider.getDepartmentStructuresPage(pagination.page, pagination.pageSize);
        setDepartmentStructures(departmentStructures.values);
        setPagination(pagination => ({ ...pagination, totalRows: departmentStructures.totalRows }));
    }

    function onAddDepartmentClick() {
        setSelectedDepartment(null);
        setIsEditDepartmentModalOpen(true);
    }

    function onSaveDepartment() {
        setIsEditDepartmentModalOpen(false);
        loadDepartmentStructuresPage();
    }

    async function removeDepartment() {
        if (selectedDepartment == null) return;

        const result = await DepartmentsProvider.removeDepartment(selectedDepartment.id);
        if (!result.isSuccess) return addErrorNotification(result.errors[0].errorMessage);

        addSuccessNotification('Успешно');
        setIsRemoveDepartmentModalOpen(false);
        loadDepartmentStructuresPage();
    }

    return (
        <Page>
            <Box marginTop={7} marginBottom={2}>
                <Button
                    variant='contained'
                    color='info'
                    onClick={onAddDepartmentClick}
                >
                    Добавить отдел
                </Button>
            </Box>
            <TableContainer component={Paper} sx={{ borderRadius: 3 }}>
                <Table>
                    <TableHead sx={{ backgroundColor: '#99CCFF' }}>
                        <TableRow>
                            <TableCell align='center'>Название</TableCell>
                            <TableCell align='center'>Номер телефона</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            departmentStructures.length !== 0
                                ? departmentStructures.map(structure => (
                                    <DepartmentTableRow
                                        key={structure.department.id}
                                        departmentStructure={structure}
                                        openDepartmentEditorModal={() => setIsEditDepartmentModalOpen(true)}
                                        openDepartmentRemoveModal={() => setIsRemoveDepartmentModalOpen(true)}
                                        setSelectedDepartment={department => setSelectedDepartment(department)}
                                        loadDepartmentStructuresPage={loadDepartmentStructuresPage}
                                    />
                                ))

                                : <TableRow>
                                    <TableCell align="center" colSpan={3}>Структура отсутствует</TableCell>
                                </TableRow>
                        }
                    </TableBody>
                </Table>
            </TableContainer >
            <Box display={'flex'} justifyContent={'center'} padding={2}>
                <CPagination
                    pageSize={pagination.pageSize}
                    onChangePage={page => setPagination(pagination => ({ ...pagination, page }))}
                    totalRows={pagination.totalRows}
                />
            </Box>
            {
                isEditDepartmentModalOpen &&
                <DepartmentEditModal
                    departmentId={selectedDepartment?.id ?? null}
                    onSave={onSaveDepartment}
                    onClose={() => setIsEditDepartmentModalOpen(false)}
                />
            }
            {
                isRemoveDepartmentModalOpen &&
                <ConfirmModal
                    title="Вы уверены, что хотите удалить отдел?"
                    onYesClick={removeDepartment}
                    onClose={() => setIsRemoveDepartmentModalOpen(false)}
                />
            }
        </Page>
    )
}