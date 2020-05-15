<template>
    <footer id="footer" class="footer">
        <div class="footer-bottom">
            <div class="container">
                <div class="footer_bottom_inner">
                    <div class="logo-footer">
                        <router-link to="/">
                            <img
                                src="@/assets/images/logo-footer.png"
                                alt="Oellfare"
                            />
                        </router-link>
                    </div>
                    <div class="copyright">
                        <p>
                            &copy; All rights reserved to
                            <a href="https://myunidays.com">UNiDAYS</a>
                        </p>
                    </div>
                    <div class="terms_nav">
                        <ul>
                            <li class="menu-item"><a href="#">Terms</a></li>
                            <li class="menu-item"><a href="#">Privacy</a></li>
                            <li class="menu-item"><a href="#">Faq</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </footer>
</template>
<style lang="sass">
#footer
  position: absolute !important
  bottom: 0
  width: 100%
  height: 5.5rem
</style>
