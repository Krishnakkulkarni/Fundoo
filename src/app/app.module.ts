import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { from } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { UserRegisterComponent } from './core/component/user-register/user-register.component';
import { LoginComponent } from './core/component/login/login.component';
import { UserComponent } from './core/component/user/user.component';
import { HomeComponent } from './core/component/home/home.component';
import { AppMaterial } from './app.material.module';
import { ForgotPasswordComponent } from './core/component/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './core/component/reset-password/reset-password.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import 'hammerjs';
import { MainNotesComponent } from './core/component/main-notes/main-notes.component';
import { NotesComponent } from './core/component/notes/notes.component';
import { DisplaynotesComponent } from './core/component/displaynotes/displaynotes.component';
import { ReminderComponent } from './core/component/reminder/reminder.component';
import { EditLabelsComponent } from './core/component/edit-labels/edit-labels.component';
import { ArchiveComponent } from './core/component/archive/archive.component';
import { TrashComponent } from './core/component/trash/trash.component';
import { IconsComponent } from './core/component/icons/icons.component';
import { EditnoteComponent } from './core/component/editnote/editnote.component';
import { ImagecropComponent } from './core/component/imagecrop/imagecrop.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { CollaborationComponent } from './core/component/collaboration/collaboration.component';
import { SearchComponent } from './core/component/search/search.component';
import { SearchPipe } from './core/pipe/search.pipe';
import { SocialLoginModule, AuthServiceConfig, FacebookLoginProvider } from "angular-6-social-login";
import { environment} from '../environments/environment';

export function getAuthServiceConfigs() {
  let config = new AuthServiceConfig(
    [
      {
        id: FacebookLoginProvider.PROVIDER_ID,
        provider: new FacebookLoginProvider(environment.getFbId)
      }
    ]
  );
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    UserRegisterComponent,
    LoginComponent,
    UserComponent,
    HomeComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent,
    MainNotesComponent,
    NotesComponent,
    DisplaynotesComponent,
    ReminderComponent,
    EditLabelsComponent,
    ArchiveComponent,
    TrashComponent,
    IconsComponent,
    EditnoteComponent,
    ImagecropComponent,
    CollaborationComponent,
    SearchComponent,
    SearchPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    AppMaterial,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ImageCropperModule,
    SocialLoginModule
  ],
  providers: [{
    provide: AuthServiceConfig,
    useFactory: getAuthServiceConfigs
  }],
  entryComponents: [EditLabelsComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
