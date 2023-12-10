export interface ActionableButtonProps {
    label: string;
    disabled?: boolean;
    onPress: (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void;
}