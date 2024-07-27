import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            let errorMessage = 'Unknown error!';

            if (error.error instanceof ErrorEvent) {
                errorMessage = `${error.error.message}`;
            } else {
                if (typeof error.error === 'string') {
                    try {
                        const errorBody = JSON.parse(error.error);
                        errorMessage = errorBody.title || 'Unknown server error';
                    } catch (e) {
                        errorMessage = 'Could not parse error response';
                    }
                } else if (error.error && typeof error.error === 'object') {
                    errorMessage = error.error.title || 'Unknown server error';
                }
            }

            return throwError(() => new Error(errorMessage));
        })
    );
};
