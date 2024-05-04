import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Home } from "./home";
import { StrictMode } from "react";
import { Links } from "../tools/constants/links";
import { Contracts } from "./contracts/contracts";
import { EmployeesPage } from "./employees/employeesPage";

export function MainRouter() {
    return (
        <StrictMode>
            <BrowserRouter>
                <Routes>
                    <Route path={Links.home} element={<Home />} />
                    <Route path={Links.contracts} element={<Contracts />} />
                    <Route path={Links.employees} element={<EmployeesPage />} />
                </Routes>
            </BrowserRouter>
        </StrictMode>
    )
}