import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, Grid, IconButton, TextField, Typography } from "@mui/material";
import ClearIcon from '@mui/icons-material/Clear';
import { useEffect, useState } from "react";
import { DepartmentBlank } from "../../domain/departments/departmentBlank";
import { DepartmentsProvider } from "../../domain/departments/departmentsProvider";
import { useNotifications } from "../../hooks/useNotifications";

interface IProps {
    departmentId: string | null;
    onSave: () => void;
    onClose: () => void;
}

export function DepartmentEditModal(props: IProps) {

    const { addErrorNotification, addSuccessNotification } = useNotifications();

    const [departmentBlank, setDepartmentBlank] = useState<DepartmentBlank>(DepartmentBlank.empty());

    useEffect(() => {
        async function init() {
            if (props.departmentId == null) return;

            const department = await DepartmentsProvider.getDepartment(props.departmentId);
            setDepartmentBlank(DepartmentBlank.fromDepartment(department));
        }

        init();
    }, [])

    console.log(departmentBlank)

    async function saveDepartment() {
        const result = await DepartmentsProvider.saveDepartment(departmentBlank);
        if (!result.isSuccess) return addErrorNotification(result.errors[0].errorMessage);

        addSuccessNotification('Успешно');
        props.onSave();
    }

    return (
        <Dialog
            open
            fullWidth
            maxWidth='sm'
            onClose={props.onClose}
        >
            <DialogTitle>
                <Box display={'flex'} alignItems={'center'} justifyContent={'space-between'}>
                    <Typography align="left" variant="h5">{props.departmentId == null ? 'Создание отдела' : 'Редактирование отдела'}</Typography>
                    <IconButton onClick={props.onClose}>
                        <ClearIcon />
                    </IconButton>
                </Box>
                <Divider sx={{ padding: 1 }} />
            </DialogTitle>
            <DialogContent>
                <Grid container spacing={2} padding={1.5}>
                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Название"
                            value={departmentBlank.name ?? ''}
                            onChange={event => setDepartmentBlank(blank => ({ ...blank, name: event.target.value }))}
                        />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Номер телефона"
                            value={departmentBlank.phoneNumber ?? ''}
                            onChange={event => setDepartmentBlank(blank => ({ ...blank, phoneNumber: event.target.value }))}
                        />
                    </Grid>
                </Grid>
            </DialogContent>
            <DialogActions>
                <Button
                    variant="contained"
                    onClick={saveDepartment}
                >
                    Сохранить
                </Button>
            </DialogActions>
        </Dialog>
    )
}