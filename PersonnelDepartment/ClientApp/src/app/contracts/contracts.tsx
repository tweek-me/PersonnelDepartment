import { useEffect } from "react";
import { ContractsProvider } from "../../domain/contracts/contractsProvider";

export function Contracts() {

    useEffect(() => {
        async function init() {
            const res = await ContractsProvider.get();
        }

        init();
    }, [])

    return (
        <>ContractsPage</>
    )
}