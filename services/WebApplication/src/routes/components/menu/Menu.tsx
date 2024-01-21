import React from "react"
import { IMenuProps, IMenuState } from "./IMenu"

const Menu = function (): JSX.Element {
    const [state, setState] = React.useState<IMenuState>({

    });

    React.useEffect(() => {
        const init = async function (): Promise<void> {

        }

        init();
    }, [])

    return (
        <>
        </>
    )
}

export default Menu;