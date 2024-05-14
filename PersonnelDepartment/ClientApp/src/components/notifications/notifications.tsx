import { AlertProps, Snackbar } from "@mui/material";
import { createContext, forwardRef, PropsWithChildren, useContext, useState } from "react";

type NotificationSeverity = 'error' | 'warning' | 'info' | 'success';

interface Notification {
    message: string;
    severity: NotificationSeverity;
}

interface NotificationContextType {
    showNotification: (message: string, severity: NotificationSeverity) => void;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const useNotification = () => {
    const context = useContext(NotificationContext);
    if (!context) {
        throw new Error('useNotification must be used within a NotificationProvider');
    }

    return context;
};

const CAlert = forwardRef<HTMLDivElement, AlertProps>(function Alert(props, ref) {
    return <Alert elevation={6} ref={ref} variant="filled" {...props} />;
});

export function NotificationProvider(props: PropsWithChildren) {
    const [notification, setNotification] = useState<Notification | null>(null);

    const showNotification = (message: string, severity: NotificationSeverity) => {
        setNotification({ message, severity });
    };

    const handleClose = () => {
        setNotification(null);
    };

    return (
        <>
            <NotificationContext.Provider value={{ showNotification }}>
                {props.children}
                <Snackbar
                    open={notification != null}
                    autoHideDuration={3000}
                    onClose={handleClose}
                    anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
                >
                    <>
                        {
                            notification != null &&
                            <CAlert onClose={handleClose} severity={notification.severity}>
                                {notification.message}
                            </CAlert>

                        }
                    </>
                </Snackbar>
            </NotificationContext.Provider>
        </>
    );
};