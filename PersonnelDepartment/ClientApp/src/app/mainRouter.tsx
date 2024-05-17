import { StrictMode } from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Links } from "../tools/constants/links";
import { Contracts } from "./contracts/contracts";
import { DepartmentPage } from "./departments/departmentPage";
import { EmployeesPage } from "./employees/employeesPage";
import { Home } from "./home";

export function MainRouter() {
    return (
        <StrictMode>
            <BrowserRouter>
                <Routes>
                    <Route path={Links.home} element={<Home />} />
                    <Route path={Links.contracts} element={<Contracts />} />
                    <Route path={Links.employees} element={<EmployeesPage />} />
                    <Route path={Links.departments} element={<DepartmentPage />} />
                </Routes>
            </BrowserRouter>
        </StrictMode>
    )
}