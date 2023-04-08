// import { Dashboard } from "@mui/icons-material";
import async from "../components/utilities/Async";
import { IRoute } from "../types/RouteType";


const Dashboard = async(() => import("../pages/dashboard/Dashboard"));
const About = async(() => import("../pages/About"));
const Config = async(() => import("../pages/Config"));
const Customer = async(() => import("../pages/baseInfo/customer/customer"));
// const DataGridTest = async(() => import("../pages/baseInfo/GenericDataGrid"));

export const routes: Array<IRoute> = [
    {
        key: 'dashboard-route',
        title: 'Dashboard',
        path: '/',
        enabled: true,
        component: Dashboard
    },
    {
        key: 'about-route',
        title: 'About',
        path: '/about',
        enabled: true,
        component: About
    },
    {
        key: 'config-route',
        title: 'Config',
        path: '/config',
        enabled: true,
        component: Config
    },
    //------------------Base Info---------------------
    {
        key: 'customer-route',
        title: 'Customer',
        path: 'baseinfo/customer',
        enabled: true,
        component: Customer
    },
    // {
    //     key: 'dataGridTest-route',
    //     title: 'dataGridTest',
    //     path: 'baseinfo/dataGridTest',
    //     enabled: true,
    //     component: DataGridTest
    // },
]

export const authRoutes: Array<IRoute> = [
    //-------------Security Authentication-------------
 ]