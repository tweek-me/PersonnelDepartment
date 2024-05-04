import ReactDOM from 'react-dom/client';
import { Notifications } from '../components/notifications/notifications';
import { MainRouter } from './mainRouter';

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <>
        <Notifications />
        <MainRouter />
    </>
);

