import { Autocomplete, Box, Button, Checkbox, Dialog, DialogActions, DialogContent, DialogTitle, Divider, FormControlLabel, Grid, IconButton, TextField, Typography } from "@mui/material";
import { ClearIcon } from "@mui/x-date-pickers";
import { useEffect, useState } from "react";
import { Department } from "../../domain/departments/department";
import { DepartmentsProvider } from "../../domain/departments/departmentsProvider";
import { EmployeeBlank } from "../../domain/employees/employeeBlank";
import { EmployeeProvider } from "../../domain/employees/employeeProvider";
import { Post } from "../../domain/posts/post";
import { PostsProvider } from "../../domain/posts/postsProvider";
import { DateTimeFormatType } from "../../tools/dates";

interface IProps {
    employeeId: string | null
    onClose: () => void;
    onSave: () => void;
}

export function EmployeeEditModal(props: IProps) {

    const [employeeBlank, setEmployeeBlank] = useState<EmployeeBlank>(EmployeeBlank.empty());
    const [departments, setDepartments] = useState<Department[]>([]);
    const [posts, setPosts] = useState<Post[]>([]);

    //TASK ILYA сделать blockUi
    useEffect(() => {
        async function init() {
            if (props.employeeId != null) {
                const employee = await EmployeeProvider.get(props.employeeId);
                setEmployeeBlank(employee);
            }

            const departments = await DepartmentsProvider.getDepartments();
            setDepartments(departments);
        }

        init();
    }, [])

    useEffect(() => {
        async function init() {
            if (employeeBlank.departmentId == null) return;

            const posts = await PostsProvider.getPosts(employeeBlank.departmentId);
            setPosts(posts);
        }

        init();
    }, [employeeBlank.departmentId])

    async function saveEmployee() {
        const result = await EmployeeProvider.save(employeeBlank);
        // if (!result.isSuccess)
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
                    <Typography align="left" variant="h5">{props.employeeId == null ? 'Создание работника' : 'Редактирование работника'}</Typography>
                    <IconButton onClick={props.onClose}>
                        <ClearIcon />
                    </IconButton>
                </Box>
                <Divider sx={{ padding: 1 }} />
            </DialogTitle>
            <DialogContent>
                <Grid container spacing={2} padding={1.5}>
                    <Grid item xs={12} md={6}>
                        <Autocomplete
                            options={departments}
                            getOptionLabel={d => d.name}
                            value={departments.find(d => d.id === employeeBlank.departmentId) ?? null}
                            renderInput={(params) => <TextField {...params} label="Отдел" />}
                            onChange={(_, department) => setEmployeeBlank(blank => ({ ...blank, departmentId: department?.id ?? null }))}
                            noOptionsText='Нет отделов'
                        />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <Autocomplete
                            disabled={employeeBlank.departmentId == null}
                            options={posts}
                            getOptionLabel={p => p.name}
                            value={posts.find(p => p.id === employeeBlank.postId) ?? null}
                            renderInput={(params) => <TextField {...params} label="Должность" />}
                            onChange={(_, post) => setEmployeeBlank(blank => ({ ...blank, postId: post?.id ?? null }))}
                            noOptionsText='Нет должностей'
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <Typography variant="h6">Личные данные</Typography>
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Фамилия"
                            value={employeeBlank.surname ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, surname: event.target.value }))}
                        />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Имя"
                            value={employeeBlank.name ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, name: event.target.value }))}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Отчество"
                            value={employeeBlank.partronymic ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, partronymic: event.target.value }))}
                        />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            type='date'
                            InputLabelProps={{ shrink: true }}
                            label="Дата рождения"
                            value={employeeBlank.birthDay?.formatWith(DateTimeFormatType.standardDate) ?? new Date().formatWith(DateTimeFormatType.standardDate)}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, birthDay: new Date(event.target.value) }))}
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <Typography variant="h6">Контактные данные</Typography>
                    </Grid>

                    {/* TASK ILYA сделать phoneInput */}
                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Номер телефона"
                            value={employeeBlank.phoneNumber ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, phoneNumber: event.target.value }))}
                        />
                    </Grid>
                    {/* TASK ILYA сделать email input */}
                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="Email"
                            value={employeeBlank.email ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, email: event.target.value }))}
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <Typography variant="h6">Документы</Typography>
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="ИНН"
                            value={employeeBlank.inn ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, inn: event.target.value }))}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            label="СНИЛС"
                            value={employeeBlank.snils ?? ''}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, snils: event.target.value }))}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            type="number"
                            InputLabelProps={{ shrink: true }}
                            label="Серия паспорта"
                            value={employeeBlank.passportSeries ?? 0}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, passportSeries: +event.target.value }))}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <TextField
                            fullWidth
                            type="number"
                            InputLabelProps={{ shrink: true }}
                            label="Номер паспорта"
                            value={employeeBlank.passportNumber ?? 0}
                            onChange={event => setEmployeeBlank(blank => ({ ...blank, passportNumber: +event.target.value }))}
                        />
                    </Grid>

                    <Grid item xs={12} md={6}>
                        <FormControlLabel control={
                            <Checkbox
                                value={employeeBlank.isDismissed}
                                onChange={_ => setEmployeeBlank(blank => ({ ...blank, isDismissed: !blank.isDismissed }))}
                            />
                        }
                            label="Уволен"
                        />
                    </Grid>

                </Grid>
            </DialogContent>
            <DialogActions>
                <Button
                    variant="contained"
                    onClick={saveEmployee}
                >
                    Сохранить
                </Button>
            </DialogActions>
        </Dialog>
    )
}