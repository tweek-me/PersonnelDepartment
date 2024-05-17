import { Box, Button, Dialog, DialogActions, DialogContent, DialogTitle, Divider, Grid, IconButton, TextField, Typography } from "@mui/material";
import { ClearIcon } from "@mui/x-date-pickers";
import { useEffect, useState } from "react";
import { PostBlank } from "../../domain/posts/postBlank";
import { PostsProvider } from "../../domain/posts/postsProvider";
import { useNotifications } from "../../hooks/useNotifications";

interface IProps {
    postId: string | null
    onClose: () => void;
    onSave: () => void;
}

export function PostEditModal(props: IProps) {
    const { addErrorNotification, addSuccessNotification } = useNotifications();

    const [postBlank, setPostBlank] = useState<PostBlank>(PostBlank.empty());

    useEffect(() => {
        async function init() {
            if (props.postId == null) return;

            const post = await PostsProvider.getPost(props.postId);
            setPostBlank(PostBlank.fromPost(post));
        }

        init();
    }, [])

    async function savePost() {
        const result = await PostsProvider.savePost(postBlank);
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
                    <Typography align="left" variant="h5">{props.postId == null ? 'Создание должности' : 'Редактирование должности'}</Typography>
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
                            value={postBlank.name ?? ''}
                            onChange={event => setPostBlank(blank => ({ ...blank, name: event.target.value }))}
                        />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField
                            type="number"
                            fullWidth
                            label="Номер телефона"
                            value={postBlank.salary}
                            onChange={event => setPostBlank(blank => ({ ...blank, salary: +event.target.value }))}
                        />
                    </Grid>
                </Grid>
            </DialogContent>
            <DialogActions>
                <Button
                    variant="contained"
                    onClick={savePost}
                >
                    Сохранить
                </Button>
            </DialogActions>
        </Dialog>
    )
}