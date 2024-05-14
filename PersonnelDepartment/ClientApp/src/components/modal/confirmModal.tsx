import { Button, Dialog, DialogActions, DialogTitle } from "@mui/material";

interface IProps {
    title: string;
    onYesClick: () => void;
    onClose: () => void;
}

export function ConfirmModal(props: IProps) {
    return (
        <Dialog open onClose={props.onClose}>
            <DialogTitle>
                {props.title}
            </DialogTitle>
            <DialogActions>
                <Button onClick={props.onYesClick}>Да</Button>
                <Button onClick={props.onClose}>Нет</Button>
            </DialogActions>
        </Dialog>
    )
}