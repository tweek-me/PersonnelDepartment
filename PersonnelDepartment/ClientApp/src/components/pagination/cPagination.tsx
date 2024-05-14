import { Pagination, SxProps } from "@mui/material"

interface IProps {
    totalRows: number
    pageSize: number
    onChangePage: (page: number) => void
    sx?: SxProps
}

export function CPagination(props: IProps) {

    const pagesCount = Math.ceil(props.totalRows / props.pageSize);

    return (
        <Pagination
            count={pagesCount}
            color="primary"
            sx={props.sx}
            hidden={props.totalRows <= props.pageSize}
            onChange={(_, page) => props.onChangePage(page)}
        />
    )
}