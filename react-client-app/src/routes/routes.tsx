import { FC }  from "react";
import { Route, Routes } from "react-router-dom";
import { Outlet } from 'react-router-dom';

import { IRoute } from "../types/RouteType";
import { routes as dashboardRoutes } from "./index";
import MainLayout from "../layouts/MainLayout";

const ModifiedMainLayout = () => {
    return (
        <MainLayout>
            <Outlet />  
        </MainLayout>
    )
    };

const AppRoutes: FC = () => {
    return (
        <>
            <Routes>
                <Route element={<ModifiedMainLayout/>}>
                {dashboardRoutes.map((route: IRoute) => (
                    <Route
                        key={route.key}
                        path={route.path}
                        element={<route.component />}
                    />
                ))}
                </Route>
            </Routes>
        </>
    )
}

export default AppRoutes;