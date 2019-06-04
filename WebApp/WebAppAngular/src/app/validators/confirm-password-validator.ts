import { ValidatorFn, FormGroup, ValidationErrors } from '@angular/forms';

export const ConfirmPasswordValidator : ValidatorFn = (control: FormGroup) : ValidationErrors | null => {
    let password = control.get('pass');
    let confirmPassword = control.get('cpass');


    

    return  password.value === confirmPassword.value ? null : {"doesntMatch" : true};
}