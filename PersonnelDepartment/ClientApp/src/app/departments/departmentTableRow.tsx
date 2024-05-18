import ClearIcon from '@mui/icons-material/Clear';
import EditIcon from '@mui/icons-material/Edit';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import PostAddIcon from '@mui/icons-material/PostAdd';
import { Box, Collapse, IconButton, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import { useState } from "react";
import { ConfirmModal } from '../../components/modal/confirmModal';
import { Department } from "../../domain/departments/department";
import { DepartmentStructure } from "../../domain/departments/departmentStructure";
import { Post } from '../../domain/posts/post';
import { PostsProvider } from '../../domain/posts/postsProvider';
import { useNotifications } from '../../hooks/useNotifications';
import { PostEditModal } from '../posts/postEditModal';

interface IProps {
    departmentStructure: DepartmentStructure;
    openDepartmentEditorModal: () => void;
    openDepartmentRemoveModal: () => void;
    setSelectedDepartment: (department: Department) => void;
    loadDepartmentStructuresPage: () => void;
}

export function DepartmentTableRow(props: IProps) {
    const department = props.departmentStructure.department;
    const posts = props.departmentStructure.posts;

    const { addSuccessNotification, addErrorNotification } = useNotifications();

    const [isEditPostModalOpen, setIsEditPostModalOpen] = useState<boolean>(false);
    const [isRemovePostModalOpen, setIsRemovePostModalOpen] = useState<boolean>(false);

    const [isRowExpanded, setIsRowExpanded] = useState<boolean>(false);
    const [selectedPost, setSelectedPost] = useState<Post | null>(null);

    function onEditDepartmentClick(department: Department) {
        props.setSelectedDepartment(department);
        props.openDepartmentEditorModal();
    }

    function onRemoveDepartmentClick(department: Department) {
        props.setSelectedDepartment(department);
        props.openDepartmentRemoveModal();
    }

    function onAddPostClick() {
        setSelectedPost(null);
        setIsEditPostModalOpen(true);
    }

    function onEditPostClick(post: Post) {
        setSelectedPost(post);
        setIsEditPostModalOpen(true);
    }

    function onRemovePostClick(post: Post) {
        setSelectedPost(post);
        setIsRemovePostModalOpen(true)
    }

    async function removePost() {
        if (selectedPost == null) return;

        const result = await PostsProvider.removePost(selectedPost.id);
        if (!result.isSuccess) addErrorNotification(result.errors[0].errorMessage);

        addSuccessNotification('Успешно');
        setIsRemovePostModalOpen(false);
        props.loadDepartmentStructuresPage();
    }

    function onSavePost() {
        setIsEditPostModalOpen(false);
        props.loadDepartmentStructuresPage();
    }

    const isHaveSubRows = posts.length !== 0;

    return (
        <>
            <TableRow key={department.id} hover>
                <TableCell align='left'>
                    {
                        isHaveSubRows
                            ? <Box display={'flex'} gap={2} alignItems={'center'}>
                                <IconButton onClick={() => isRowExpanded ? setIsRowExpanded(false) : setIsRowExpanded(true)}>
                                    {
                                        isRowExpanded
                                            ? <ExpandLessIcon />
                                            : <ExpandMoreIcon />
                                    }
                                </IconButton>
                                <Box>{department.name}</Box>
                            </Box>

                            : <Box paddingLeft={7}>{department.name}</Box>
                    }
                </TableCell>
                <TableCell align='center'>{department.phoneNumber}</TableCell>
                <TableCell>
                    <Box justifyContent={'flex-end'} display={'flex'} gap={2}>
                        <IconButton onClick={() => onEditDepartmentClick(department)}>
                            <EditIcon />
                        </IconButton>
                        <IconButton onClick={() => onRemoveDepartmentClick(department)}>
                            <ClearIcon />
                        </IconButton>
                        <IconButton onClick={() => onAddPostClick()}>
                            <PostAddIcon />
                        </IconButton>
                    </Box>
                </TableCell>
            </TableRow>

            <TableCell sx={{ padding: 0 }} colSpan={3}>
                <Collapse in={isRowExpanded}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: 'bold', paddingLeft: 9 }}>Название</TableCell>
                                <TableCell sx={{ fontWeight: 'bold', paddingLeft: 9 }}>Зарплата</TableCell>
                                <TableCell></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {
                                posts.map(post => (
                                    <TableRow key={post.id} sx={{ '& > *': { borderBottom: 'unset' } }}>
                                        <TableCell sx={{ paddingLeft: 9 }}>{post.name}</TableCell>
                                        <TableCell sx={{ paddingLeft: 9 }}>{post.salary}</TableCell>
                                        <TableCell>
                                            <Box justifyContent={'flex-end'} display={'flex'} gap={2}>
                                                <IconButton onClick={() => onEditPostClick(post)}>
                                                    <EditIcon />
                                                </IconButton>
                                                <IconButton onClick={() => onRemovePostClick(post)}>
                                                    <ClearIcon />
                                                </IconButton>
                                            </Box>
                                        </TableCell>
                                    </TableRow>
                                ))
                            }
                        </TableBody>
                    </Table>
                </Collapse >
            </TableCell>
            {
                isEditPostModalOpen &&
                <PostEditModal
                    postId={selectedPost?.id ?? null}
                    departmentId={department.id}
                    onSave={onSavePost}
                    onClose={() => setIsEditPostModalOpen(false)}
                />
            }
            {
                isRemovePostModalOpen &&
                <ConfirmModal
                    title='Вы уверены, что хотите удалить должность?'
                    onYesClick={removePost}
                    onClose={() => setIsRemovePostModalOpen(false)}
                />
            }
        </>
    )
}